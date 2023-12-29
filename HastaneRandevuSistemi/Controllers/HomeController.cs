using HastaneRandevuSistemi.Data;
using HastaneRandevuSistemi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace HastaneRandevuSistemi.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

		public IActionResult Index()
		{
			return View();
		}
        public IActionResult Admin()
        {
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
                return RedirectToAction("Index", "Hastane");
            }
            TempData["hata"] = "Kullanıcı adı veya şifre hatalı";
            return RedirectToAction("Admin");
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
        
    }
}
