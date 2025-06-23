using AdminDyanamoEnterprises.DTOs.Master;
using AdminDyanamoEnterprises.Repository;
using AdminDyanamoEnterprises.Repository.IRepository;
using AdminDyanamoEnterprises.Repository.Repository;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdminDyanamoEnterprises.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountRepositary AccountRepositary;
        private readonly INotyfService _notyf;

        public AccountController(IAccountRepositary iaccountrepository, INotyfService notyf)
        {
            _iAccountrepository = iaccountrepository;
            _notyf = notyf;

        }
        // GET: AccountController
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Login(LoginType model)
        {
            if (ModelState.IsValid)
            {

                bool isValidUser = _accountRepository.CheckLogin(model);
                if (isValidUser)
                {
                    // You can set session/cookies here as needed
                    return RedirectToAction("Master", "Account");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid email or password");
                }
            }

            return View(model);
        }

        // GET: AccountController/Details/5
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
