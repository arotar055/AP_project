using AP_project.Models;
using AP_project.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace AP_project.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService accountServ;
        public AccountController(IAccountService accountService)
        {
            accountServ = accountService;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginAjax([FromForm] LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                var allErrors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .Where(x => !string.IsNullOrWhiteSpace(x))
                    .ToArray();

                return Json(new { success = false, errors = allErrors });
            }

            var user = await accountServ.ValidateUserAsync(model);
            if (user == null)
            {
                return Json(new
                {
                    success = false,
                    errors = new[] { "Неверный логин или пароль" }
                });
            }

            HttpContext.Session.SetString("UserLogin", user.Name!);
            HttpContext.Session.SetInt32("UserId", user.Id);
            HttpContext.Session.Remove("IsGuest");

            return Json(new { success = true, message = "Вход выполнен успешно" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterAjax([FromForm] RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                var allErrors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .Where(x => !string.IsNullOrWhiteSpace(x))
                    .ToArray();

                return Json(new { success = false, errors = allErrors });
            }

            try
            {
                await accountServ.RegisterUserAsync(model);
                return Json(new { success = true, message = "Регистрация прошла успешно" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, errors = new[] { ex.Message } });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult GuestLoginAjax()
        {
            HttpContext.Session.SetString("IsGuest", "true");
            HttpContext.Session.Remove("UserLogin");
            HttpContext.Session.Remove("UserId");
            return Json(new { success = true, message = "Вход как гость выполнен" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult LogoutAjax()
        {
            HttpContext.Session.Clear();
            return Json(new { success = true, message = "Вы вышли из системы" });
        }
    }
}