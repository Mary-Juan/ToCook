using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Xml.Linq;
using ToCook.Core.Convertors;
using ToCook.Core.DTOs;
using ToCook.Core.Generators;
using ToCook.Core.Security;
using ToCook.Core.Services.Interfaces;
using ToCook.DataLayer.Entities.User;

namespace ToCook.Web.Controllers
{
    public class AccountController : Controller
    {
        private IUserService _userService;
        private IViewRenderService _renderViewToString;
        private IEmailService _emailService;

        public AccountController(IUserService userService, IViewRenderService renderViewToString, IEmailService emailService)
        {
            _userService = userService;
            _renderViewToString = renderViewToString;
            _emailService = emailService;
        }


        #region Register

        [Route("Register")]
        public async Task<IActionResult> Register() => View();


        [Route("Register")]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel register)
        {
            if (!ModelState.IsValid)
                return View(register);

            if (_userService.IsExistEmail(FixedText.FixEmail(register.Email)))
            {
                ModelState.AddModelError("Email", "ایمیل تکراری میباشد.");
                return View(register);
            }

            if (_userService.IsExistUserName(register.UserName))
            {
                ModelState.AddModelError("UserName", "نام کاربری قبلا استفاده شده است.");
                return View(register);
            }

            User user = new User()
            {
                UserName = register.UserName,
                Email = FixedText.FixEmail(register.Email),
                ActiveCode = NameGenerator.GenerateUniqueCode(),
                IsActive = false,
                Password = PasswordHelper.EncodePasswordMd5(register.Password),
                RegisterDate = DateTime.Now,
                UserAvatar = "DefaultProf.png"
            };

            _userService.AddUser(user);

            #region Send Activation Email

            var email = new EmailDTO()
            {
                To = user.Email,
                Body = _renderViewToString.RenderToStringAsync("_ActiveEmail", user),
                Subject = " فعالسازی حساب"
            };

            _emailService.SendEmail(email);

            #endregion

            return View("SuccessRegister", user);
        }

        #endregion

        #region Login
        [Route("Login")]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Route("Login")]
        public ActionResult Login(LoginViewModel login)
        {
            if (!ModelState.IsValid)
            {
                return View(login);
            }

            var user = _userService.LoginUser(login);
            if (user != null)
            {
                if (user.IsActive)
                {
                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.NameIdentifier,user.UserId.ToString()),
                        new Claim(ClaimTypes.Name,user.UserName)
                    };
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);

                    var properties = new AuthenticationProperties
                    {
                        IsPersistent = login.RememberMe
                    };
                    HttpContext.SignInAsync(principal, properties);

                    ViewBag.IsSuccess = true;
                    return View();
                }
                else
                {
                    ModelState.AddModelError("Email", "حساب کاربری شما فعال نمی باشد");
                }
            }
            ModelState.AddModelError("Email", "کاربری با مشخصات وارد شده یافت نشد");
            return View(login);
        }

        #endregion

        #region Logout
        [Route("Logout")]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/Login");
        }

        #endregion


    }
}
