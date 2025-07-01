using AdminDyanamoEnterprises.DTOs;
using AdminDyanamoEnterprises.DTOs.Account;
using AdminDyanamoEnterprises.Repository;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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


        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginType model)
        {     
                if (ModelState.IsValid)
                {
                    bool isValidUser = _iaccountRepository.CheckLogin(model);
                    if (isValidUser)
                    {
                        _notyf.Success("Login successful!");
                        return RedirectToAction("Dashboard", "Master");
                    }
                }
                else
                {
                    _notyf.Error("Invalid email or password.");
                    ModelState.AddModelError("", "Invalid email or password");
                }
            
            return View();
        }

        public IActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Registration(RegisterType registerType)
        {
            if (ModelState.IsValid)
            {
                var result = _iaccountRepository.RegisterUser(registerType);

                if (result.ErrorCode == 0)
                {
                   
                    _notyf.Success(result.ErrorMessage);
                    return RedirectToAction("Login");
                }
                else
                {
                    ModelState.AddModelError(nameof(registerType.Emailid), result.ErrorMessage);
                }
            }

            return View(registerType);
        }

    }
}
