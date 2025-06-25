using AdminDyanamoEnterprises.DTOs;
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
                        return RedirectToAction("CategoryType", "Master");
                    }
                }
                else
                {
                    _notyf.Error("Invalid email or password.");
                    ModelState.AddModelError("", "Invalid email or password");
                }
            
            return View();
        }
    }
}
