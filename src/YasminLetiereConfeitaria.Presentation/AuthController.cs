using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using YasminLetiereConfeitaria.Application.Interfaces;

namespace YasminLetiereConfeitaria.Presentation
{
    public class AuthController(IAuthAppService authAppService) : Controller
    {
        private readonly IAuthAppService _authAppService = authAppService;

        [HttpGet]
        public IActionResult Login()
        {
            // Se já estiver logado, redireciona para o admin
            if (Request.Cookies.ContainsKey("jwt"))
            {
                return RedirectToAction("Index", "Admin");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password, bool lembrar = false)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                ViewBag.Error = "Por favor, preencha todos os campos.";
                return View();
            }

            var token = await _authAppService.LoginAsync(username, password);
            if (token == null)
            {
                ViewBag.Error = "Usuário ou senha inválidos.";
                return View();
            }

            // Grava o JWT em um cookie HttpOnly seguro
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = lembrar ? DateTimeOffset.UtcNow.AddDays(30) : DateTimeOffset.UtcNow.AddMinutes(120)
            };

            Response.Cookies.Append("jwt", token, cookieOptions);

            return RedirectToAction("Index", "Admin");
        }

        [HttpGet]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("jwt");
            return RedirectToAction("Index", "Home");
        }
    }
}
