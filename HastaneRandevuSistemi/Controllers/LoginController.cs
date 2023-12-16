using HastaneRandevuSistemi.Models;
using Microsoft.AspNetCore.Mvc;

namespace HastaneRandevuSistemi.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index() 
        { 
            return View(); 
        } 
        [HttpPost]
        public IActionResult GirisYap([Bind("KimlikNo,Sifre")] Hasta hasta)
        {
            if (ModelState.IsValid)
            {
                
            }

            return View(hasta);
        }
        public IActionResult DoktorGiris([Bind("KimlikNo,Sifre")] Doktor doktor)
        {
            if (ModelState.IsValid)
            {

            }

            return View(doktor);
        }
    }
}
