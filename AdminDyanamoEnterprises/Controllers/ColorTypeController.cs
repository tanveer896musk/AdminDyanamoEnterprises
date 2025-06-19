
using AdminDyanamoEnterprises.DTOs.Master;
using AdminDyanamoEnterprises.Repository.IRepository;
using AdminDyanamoEnterprises.Repository.Repository;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace AdminDyanamoEnterprises.Controllers
{
    public class ColorTypeController : Controller
    {
        private readonly IColorRepository _icolorrepository;
        private readonly INotyfService _notyf;

        public ColorTypeController(IColorRepository icolorrepository, INotyfService notyf)
        {
            _icolorrepository = icolorrepository;
            _notyf = notyf;
        }

        // GET: ColorType
        public ActionResult Index()
        {
            return View();
        }

        // GET: Colorfabric typeController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ColorType/ColorType
        public ActionResult ColorType()
        {
            ColorTypePageViewModel model = new ColorTypePageViewModel
            {
                AddColor = new AddColorType(), // Empty form
                ColorList = _icolorrepository.GetAllListType() // ✅ Correct usage
            };
            return View(model);
        }

        // POST: FabricType/FabricType
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ColorType(ColorTypePageViewModel addColorType)
        {
            try
            {
                _icolorrepository.Sp_InsertOrUpdateOrDeleteColor(addColorType); // ✅ Correct usage
                _notyf.Success("Color type saved successfully!");
                return RedirectToAction("ColorType");
            }
            catch
            {
                _notyf.Error("An error occurred while saving.");
                return View(addColorType);
            }
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            try
            {
                _icolorrepository.DeleteColor(id);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
        }
    }
}

