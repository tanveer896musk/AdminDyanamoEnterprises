using AdminDyanamoEnterprises.DTOs;
using AdminDyanamoEnterprises.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
            return View();
        }

        // POST: MasterController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CategoryType(AddCategoryType addCategoryType)
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
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MasterController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
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
    }
}
