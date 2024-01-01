using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HastaneRandevuSistemi.Data;
using HastaneRandevuSistemi.Models;

namespace HastaneRandevuSistemi.Controllers
{
    public class RandevuController : Controller
    {
        private readonly HastaneRandevuSistemiContext _context;

        public RandevuController(HastaneRandevuSistemiContext context)
        {
            _context = context;
        }

        // GET: Randevu
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("UserRole") != "Admin")
            {
                return RedirectToAction("Index", "Home");
            }
            var hastaneRandevuSistemiContext = _context.Randevu.Include(r => r.Hasta).Include(r => r.doktor);
            return View(await hastaneRandevuSistemiContext.ToListAsync());
        }

        // GET: Randevu/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (HttpContext.Session.GetString("UserRole") != "Admin")
            {
                return RedirectToAction("Index", "Home");
            }
            if (id == null || _context.Randevu == null)
            {
                return NotFound();
            }

            var randevu = await _context.Randevu
                .Include(r => r.Hasta)
                .Include(r => r.doktor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (randevu == null)
            {
                return NotFound();
            }

            return View(randevu);
        }

        // GET: Randevu/Create
        public IActionResult RandevuAl()
        {
            if (HttpContext.Session.GetString("UserRole") != "Hasta")
            {
                return RedirectToAction("Index", "Home");
            }
            
            ViewData["SehirId"] = new SelectList(_context.Sehir, "Id", "SehirIsmi");
            return View();
        }

        // POST: Randevu/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> RandevuOlustur(string doktorId, string date, string hour)
        {
            int HastaId = HttpContext.Session.GetInt32("SessionId").GetValueOrDefault();
            DateTime randevuTarihi = DateTime.Parse(date + " " + hour);
            int DoktorId = int.Parse(doktorId);
            if (randevuTarihi < DateTime.Now)
                return BadRequest(new { message = "Randevu tarihi geçersiz." });
            if (_context.Randevu.Any(r => r.HastaId == HastaId && r.RandevuTarihi>DateTime.Now))
                return BadRequest(new { message = "Randevu oluşturma başarısız. Zaten randevunuz bulunmaktadır." });
            var _doktor = await _context.Doktor.FindAsync(DoktorId);
            var _hasta = await _context.Hasta.FindAsync(HastaId);
            var randevu=new Randevu { DoktorId=DoktorId, HastaId=HastaId, RandevuTarihi=randevuTarihi, doktor = _doktor, Hasta=_hasta};
            _context.Add(randevu);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Randevu başarıyla oluşturuldu." });
        }
       

        // GET: Randevu/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (HttpContext.Session.GetString("UserRole") != "Admin")
            {
                return RedirectToAction("Index", "Home");
            }
            if (id == null || _context.Randevu == null)
            {
                return NotFound();
            }

            var randevu = await _context.Randevu.FindAsync(id);
            if (randevu == null)
            {
                return NotFound();
            }
            ViewData["HastaId"] = new SelectList(_context.Hasta, "Id", "Isim", randevu.HastaId);
            ViewData["DoktorId"] = new SelectList(_context.Doktor, "Id", "Isim", randevu.DoktorId);
            return View(randevu);
        }

        // POST: Randevu/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,HastaId,RandevuTarihi,DoktorId")] Randevu randevu)
        {
            if (HttpContext.Session.GetString("UserRole") != "Admin")
            {
                return RedirectToAction("Index", "Home");
            }
            if (id != randevu.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(randevu);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RandevuExists(randevu.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["HastaId"] = new SelectList(_context.Hasta, "Id", "Isim", randevu.HastaId);
            ViewData["DoktorId"] = new SelectList(_context.Doktor, "Id", "Isim", randevu.DoktorId);
            return View(randevu);
        }

        // GET: Randevu/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var _randevu = _context.Randevu.Where(r => r.Id == id).FirstOrDefault();
            if (HttpContext.Session.GetString("UserRole") != "Admin" && HttpContext.Session.GetInt32("SessionId") != _randevu.HastaId)
            {
                return RedirectToAction("Index", "Home");
            }
            if (id == null || _context.Randevu == null)
            {
                return NotFound();
            }

            var randevu = await _context.Randevu
                .Include(r => r.Hasta)
                .Include(r => r.doktor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (randevu == null)
            {
                return NotFound();
            }

            return View(randevu);
        }

        // POST: Randevu/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var _randevu = _context.Randevu.Where(r => r.Id == id).FirstOrDefault();
            if (HttpContext.Session.GetString("UserRole") != "Admin" && HttpContext.Session.GetInt32("SessionId") != _randevu.HastaId)
            {
                return RedirectToAction("Index", "Home");
            }
            if (_context.Randevu == null)
            {
                return Problem("Entity set 'HastaneRandevuSistemiContext.Randevu'  is null.");
            }
            var randevu = await _context.Randevu.FindAsync(id);
            if (randevu != null)
            {
                _context.Randevu.Remove(randevu);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RandevuExists(int id)
        {
          return (_context.Randevu?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        [HttpGet]
        public IActionResult DoluSaatler(string selectedDate, string doktorId)
        {
            DateTime parsedDate = DateTime.Parse(selectedDate);
            int DoktorId = int.Parse(doktorId);
            // Belirli bir tarihe ait uygun randevu saatlerini çekme
            var reservedHours = _context.Randevu
                .Where(r => r.RandevuTarihi.Date == parsedDate.Date && r.DoktorId == DoktorId)
                .Select(r => new { Hour = r.RandevuTarihi.Hour, Minute = r.RandevuTarihi.Minute })
                .ToList();

            if (parsedDate.Date == DateTime.Today)
            {
                var currentDateTime = DateTime.Now;
                var currentTime = currentDateTime.Hour;

                for (int hour = 8; hour <= currentTime; hour++)
                {
                    for (int minute = 0; minute < 60; minute += 10)
                    {
                        reservedHours.Add(new { Hour = hour, Minute = minute });
                    }
                }
            }
            return Json(reservedHours);
            
        }
        [HttpGet]
        public IActionResult GetIlceler(int sehirId)
        {
            var ilceler = _context.Ilce.Where(ilce => ilce.SehirId == sehirId).ToList();
            return Json(ilceler);
        }
        public IActionResult GetHastaneler(int ilceId)
        {
            var hastaneler = _context.Hastane.Where(hastane => hastane.IlceId == ilceId).ToList();
            return Json(hastaneler);
        }
        public IActionResult GetPoliklinikler(int hastaneId)
        {
            var poliklinikler = _context.Poliklinik.Where(poliklinik => poliklinik.HastaneId == hastaneId).ToList();
            return Json(poliklinikler);
        }
        public IActionResult GetDoktorlar(int poliklinikId)
        {
            var doktorlar = _context.Doktor.Where(doktor => doktor.poliklinikId == poliklinikId).ToList();
            return Json(doktorlar);
        }
    }
}
