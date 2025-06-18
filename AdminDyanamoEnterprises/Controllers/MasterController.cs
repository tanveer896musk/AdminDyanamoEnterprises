using AdminDyanamoEnterprises.DTOs;
using AdminDyanamoEnterprises.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace AdminDyanamoEnterprises.Controllers
{
    public class MasterController : Controller
    {
        private readonly IMasterRepository _imasterrepository;

        public MasterController(IMasterRepository imasterrepository)
        {
            _imasterrepository = imasterrepository;
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
                CategoryList = _imasterrepository.GetAllListType() // From database or service
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
                _imasterrepository.AddCategory(addCategoryType);
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: MasterController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MasterController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MasterController/Delete/5
        /*  public ActionResult Delete(int id)
          {
              return View();
          }*/

        // POST: MasterController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id,DeleteCategoryType dt)
        {
            dt.CategoryID = id;
            _imasterrepository.DeleteCategory(dt);
            return Json(new { success = true });
        }

    }
}
