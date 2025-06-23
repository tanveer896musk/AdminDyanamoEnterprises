using AdminDyanamoEnterprises.DTOs;
using AdminDyanamoEnterprises.DTOs.Master;
using AdminDyanamoEnterprises.Repository.IRepository;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using static AdminDyanamoEnterprises.Repository.AccountRepository;

namespace AdminDyanamoEnterprises.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountRepository _iaccountRepository;
        private readonly INotyfService _notyf;
       public AccountController(

           IAccountRepository iaccountRepository,
           INotyfService notyf
           )
        {
            _iaccountRepository = iaccountRepository;
            _notyf = notyf; 
        }

        // GET: AccountController
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Account model)
        {
            if (ModelState.IsValid)
            {

                bool isValidUser = _iaccountRepository.CheckLogin(model);
                if (isValidUser)
                {
                    // You can set session/cookies here as needed
                   /* HttpContext.Session.SetString("Email", model.Emailid);*/
                    _notyf.Success("Login successful!");
                    return RedirectToAction("Master", "Account");
                }
                else
                {
                    _notyf.Error("Invalid email or password.");
                    ModelState.AddModelError("", "Invalid email or password");
                }
            }
            return View(model);
        }

        // GET: AccountController/Details/5
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AccountController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AccountController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: AccountController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AccountController/Edit/5
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

        // GET: AccountController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AccountController/Delete/5
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
