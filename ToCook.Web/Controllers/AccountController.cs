using Microsoft.AspNetCore.Mvc;
using ToCook.Core.Convertors;
using ToCook.Core.DTOs;
using ToCook.Core.Services.Interfaces;

namespace ToCook.Web.Controllers
{
    public class AccountController : Controller
    {
        private IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }


        [Route("Register")]
        public async Task<IActionResult> Register() => View();
       

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(RegisterViewModel register)
        {
            if (!ModelState.IsValid)
            {
                return View(register);
            }


            if (_userService.IsExistUserName(register.UserName))
            {
                ModelState.AddModelError("UserName", "نام کاربری معتبر نمی باشد");
                return View(register);
            }

            if (_userService.IsExistEmail(FixedText.FixEmail(register.Email)))
            {
                ModelState.AddModelError("Email", "ایمیل معتبر نمی باشد");
                return View(register);
            }


            //TODO: Register User

            return View();
        }
    }
}
