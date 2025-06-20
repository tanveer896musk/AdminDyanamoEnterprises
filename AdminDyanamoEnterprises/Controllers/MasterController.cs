using AdminDyanamoEnterprises.DTOs;
using AdminDyanamoEnterprises.DTOs.Master;
using AdminDyanamoEnterprises.Repository;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace AdminDyanamoEnterprises.Controllers
{
    public class MasterController : Controller
    {
        private readonly IMasterRepository _imasterrepository;
        private readonly INotyfService _notyf;

        public MasterController(IMasterRepository imasterrepository, INotyfService notyf)
        {
            _imasterrepository = imasterrepository;
            _notyf = notyf;
        }
        // GET: MasterController
        public ActionResult Index()
        {

            return View();
        }

        // GET: MasterController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MasterController/Category
        public ActionResult CategoryType()
        {
            CategoryTypePageViewModel model = new CategoryTypePageViewModel
            {
                AddCategory = new AddCategoryType(), // Empty form
                CategoryList = _imasterrepository.GetAllCategoryType() // From database or service
            };
            return View(model);
            /* return View(model);*/
        }

        // POST: MasterController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CategoryType(CategoryTypePageViewModel addCategoryType)
        {
            try
            {
                _imasterrepository.InsertorUpdateCategoryType(addCategoryType);
                _notyf.Success("Success ");
                return RedirectToAction("CategoryType");
            }
            catch
            {
                return View();
            }
        }


        [HttpPost]
        public IActionResult Delete(int id)
        {
            _imasterrepository.DeleteCategory(id);
            return Json(new { success = true });
        }

        public ActionResult SubCategoryType()
        {
            SubCategoryTypeJoinModel model = new SubCategoryTypeJoinModel();
            model.CategoryList = _imasterrepository.GetAllCategoryType();
            /*model.SubCategoryList = _imasterrepository.GetAllSubCategoriesWithCategoryName(); */// Join query
            model.SubCategoryList = _imasterrepository.GetAllSubCategoriesWithCategoryName();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SubCategoryType(SubCategoryTypeJoinModel model)
        {
            if (model.SubAddCategory != null)
            {
                _imasterrepository.InsertOrUpdateSubCategory(model.SubAddCategory);
                _notyf.Success("Sub-Category saved successfully.");
            }

            return RedirectToAction("SubCategoryType");
        }

        public ActionResult PatternType()
        {
            PatternTypePageViewModel model = new PatternTypePageViewModel
            {
                AddPattern = new AddPatternType(), // Empty form
                PatternList = _imasterrepository.GetAllPatternType() // From database or service
            };
            return View(model);
            /* return View(model);*/
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PatternType(PatternTypePageViewModel addPatternType)
        {
            try
            {
                _imasterrepository.InsertOrUpdateOrDeletePattern(addPatternType);
                _notyf.Success("Success ");
                return RedirectToAction("PatternType");
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public IActionResult DeletePattern(int id)
        {
            _imasterrepository.DeletePattern(id);
            return Json(new { success = true });
        }
        [HttpPost]
        public JsonResult DeleteSubCategory(int id)
        {
            try
            {
                _imasterrepository.DeleteSubCategory(id);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }


    }
}
