using AdminDyanamoEnterprises.DTOs;
using AdminDyanamoEnterprises.Repository;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdminDyanamoEnterprises.Controllers
{
    public class ProductController : Controller
    {
        private readonly IMasterRepository _imasterrepository;
        private readonly IProductRepository _iproductrepository;
        private readonly INotyfService _notyf;

        public ProductController(
            IMasterRepository imasterrepository,
            IProductRepository iproductrepository,
            INotyfService notyf
            )
        {
            _imasterrepository = imasterrepository;
            _iproductrepository = iproductrepository;
            _notyf = notyf;
        }

        // GET: ProductController
        public ActionResult Index()
        {
            var model = new ProductPageViewModel
            {
                ProductList = _iproductrepository.GetAllProducts()
            };
            return View(model);
        }

        // GET: ProductController/Details/5
        public ActionResult Details(int id)
        {
            var product = _iproductrepository.GetProductById(id);
            if (product == null)
            {
                _notyf.Error("Product not found.");
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: ProductController/AddProduct
        public ActionResult AddProduct()
        {
            // Following the same pattern as MasterController
            ProductPageViewModel model = new ProductPageViewModel
            {
                AddProduct = new AddProductType() { IsActive = true }, // Empty form with default active status
                
                // Load all master data for dropdowns - same as MasterController pattern
                CategoryList = _imasterrepository.GetAllCategoryType(),
                SubCategoryList = _imasterrepository.GetAllSubCategoriesWithCategoryName(),
                ColorList = _imasterrepository.GetAllListColorType(),
                FabricList = _imasterrepository.GetAllListFabricType(),
                PatternList = _imasterrepository.GetAllPatternType(),
                MaterialList = _imasterrepository.GetAllListMaterialType()
            };
            
            return View(model);
        }

        // AJAX: Get subcategories by category ID
        [HttpGet]
        public JsonResult GetSubCategoriesByCategory(int categoryId)
        {
            try
            {
                var subCategories = _imasterrepository.GetSubCategoriesByCategoryId(categoryId);
                var result = subCategories.Select(s => new {
                    SubCategoryID = s.SubCategoryID,
                    SubCategoryName = s.SubCategoryName
                }).ToList();
                
                return Json(result);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }

        // POST: ProductController/AddProduct
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddProduct(ProductPageViewModel model)
        {
            try
            {
                bool isEdit = model.AddProduct.ProductID > 0;
                var result = _iproductrepository.InsertOrUpdateProduct(model.AddProduct);
                
                if (result.ErrorCode == 0)
                {
                    _notyf.Success(result.ReturnMessage);
                    // Redirect to Index after successful add/edit to show the updated list
                    return RedirectToAction("Index");
                }
                else
                {
                    _notyf.Error(result.ReturnMessage);
                }
            }
            catch (Exception ex)
            {
                _notyf.Error("Something went wrong: " + ex.Message);
            }
            
            // Reload master data on error - same pattern as MasterController
            model.CategoryList = _imasterrepository.GetAllCategoryType();
            model.SubCategoryList = _imasterrepository.GetAllSubCategoriesWithCategoryName();
            model.ColorList = _imasterrepository.GetAllListColorType();
            model.FabricList = _imasterrepository.GetAllListFabricType();
            model.PatternList = _imasterrepository.GetAllPatternType();
            model.MaterialList = _imasterrepository.GetAllListMaterialType();
            
            return View(model);
        }

        // GET: ProductController/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                var product = _iproductrepository.GetProductForEdit(id);
                if (product == null)
                {
                    _notyf.Error("Product not found.");
                    return RedirectToAction("Index");
                }

                ProductPageViewModel model = new ProductPageViewModel
                {
                    AddProduct = product,
                    
                    // Load all master data for dropdowns
                    CategoryList = _imasterrepository.GetAllCategoryType(),
                    SubCategoryList = _imasterrepository.GetAllSubCategoriesWithCategoryName(),
                    ColorList = _imasterrepository.GetAllListColorType(),
                    FabricList = _imasterrepository.GetAllListFabricType(),
                    PatternList = _imasterrepository.GetAllPatternType(),
                    MaterialList = _imasterrepository.GetAllListMaterialType()
                };
                
                // Use the AddProduct view for editing
                return View("AddProduct", model);
            }
            catch (Exception ex)
            {
                _notyf.Error("Error loading product: " + ex.Message);
                return RedirectToAction("Index");
            }
        }

        // POST: ProductController/Edit/5 - Redirect to AddProduct POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ProductPageViewModel model)
        {
            // Ensure the ProductID is set correctly for update
            model.AddProduct.ProductID = id;
            
            // Call the AddProduct POST method which handles both insert and update
            return AddProduct(model);
        }

        // POST: ProductController/Delete - AJAX Delete like CategoryType
        [HttpPost]
        public IActionResult Delete(int id)
        {
            try
            {
                var result = _iproductrepository.DeleteProduct(id);
                
                if (result.ErrorCode == 0)
                {
                    return Json(new { success = true, message = result.ReturnMessage });
                }
                else
                {
                    return Json(new { success = false, message = result.ReturnMessage });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error deleting product: " + ex.Message });
            }
        }
    }
}
