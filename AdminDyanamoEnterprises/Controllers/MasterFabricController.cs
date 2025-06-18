using AdminDyanamoEnterprises.DTOs;
using AdminDyanamoEnterprises.DTOs.Master;
using AdminDyanamoEnterprises.Repository;
using AdminDyanamoEnterprises.Repository.IRepository;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdminDyanamoEnterprises.Controllers
{
    public class MasterFabricController : Controller
    {
        private readonly IMasterFabricRepository _imasterFabricRepository;
        private readonly INotyfService _notyf;

        public MasterFabricController(IMasterFabricRepository imasterFabricRepository, INotyfService notyf)
        {
            _imasterFabricRepository = imasterFabricRepository;
            _notyf = notyf;
        }
        // GET: MasterFabricController
        public ActionResult Index()
        {
            return View();
        }

        // GET: MasterFabricController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MasterFabricController/Create
        public ActionResult FabricType()
        {
            FabricTypePageViewModel viewModel = new FabricTypePageViewModel()
            {
                AddFabric = new AddFabricType(),
                FabricList = _imasterFabricRepository.GetAllListType()

            };
            return View();
        }

        // POST: MasterFabricController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FabricType(FabricTypePageViewModel addFabricType)
        {
            try
            {
                _imasterFabricRepository.InsertOrUpdateOrDeleteFabric(addFabricType);
                _notyf.Success("Success ");
                return RedirectToAction("FabricType");
                
            }
            catch
            {
                return View();
            }
        }

        // GET: MasterFabricController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MasterFabricController/Edit/5
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
        [HttpPost]
        public IActionResult Delete(int id)
        {
            _imasterFabricRepository.DeleteFabric(id);
            return Json(new { success = true });
        }

        public ActionResult MasterFabricType()
        {

            return View();

        }
    }
}
