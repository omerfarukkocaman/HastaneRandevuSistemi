using HastaneRandevuSistemi.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HastaneRandevuSistemi.Controllers
{
    public class CallRandevuApiController : Controller
    {
        public async Task<IActionResult> Index()
        {
            List<Randevu> randevular = new List<Randevu>();
            HttpClient client = new HttpClient();
            var response = await client.GetAsync("https://localhost:7246/api/RandevuApi");
            var jsonResponse = await response.Content.ReadAsStringAsync();
            randevular = JsonConvert.DeserializeObject<List<Randevu>>(jsonResponse);


            return View(randevular);
        }
    }
}
