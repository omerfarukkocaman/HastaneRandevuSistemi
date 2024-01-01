using HastaneRandevuSistemi.Data;
using HastaneRandevuSistemi.Models;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WebApplication3.Services;

namespace HastaneRandevuSistemi.Controllers
{
	public class HomeController : Controller
	{

		private readonly ILogger<HomeController> _logger;
        private LanguageService _localization;

        public HomeController(ILogger<HomeController> logger, LanguageService localization)
        {
            _logger = logger;
            _localization=localization;
        }

		public IActionResult Index()
		{
            ViewBag.Welcome = _localization.Getkey("Welcome").Value;
            var currentCulture = Thread.CurrentThread.CurrentCulture.Name;
            return View();
		}
        public IActionResult ChangeLanguage(string culture)
        {
            Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)), new CookieOptions()
                {
                    Expires = DateTimeOffset.UtcNow.AddYears(1)
                });
            return Redirect(Request.Headers["Referer"].ToString());
        }
        public IActionResult Admin()
        {
            return View();
        }
        public IActionResult AdminIndex()
        {
            if (HttpContext.Session.GetString("UserRole") != "Admin")
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        public IActionResult AdminGiris()
        {
            var Email = HttpContext.Request.Form["Email"];
            var Sifre = HttpContext.Request.Form["Sifre"];
            if (Email == "g221210377@sakarya.edu.tr" && Sifre == "sau")
            {
                HttpContext.Session.SetString("SessionUser", "Ömer Faruk Kocaman");
                var cookieOpt = new CookieOptions
                {
                    Expires = DateTime.Now.AddMinutes(20)
                };
                HttpContext.Response.Cookies.Append("CookieID", "g221210377", cookieOpt);
                HttpContext.Session.SetString("UserRole", "Admin");
                return RedirectToAction("AdminIndex", "Home");
            }
            TempData["hata"] = "Kullanıcı adı veya şifre hatalı";
            return RedirectToAction("AdminIndex");
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
        
    }
}
