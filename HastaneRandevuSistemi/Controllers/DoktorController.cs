using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HastaneRandevuSistemi.Data;
using HastaneRandevuSistemi.Models;
using Microsoft.AspNetCore.Authorization;

namespace HastaneRandevuSistemi.Controllers
{
    public class DoktorController : Controller
    {
        private readonly HastaneRandevuSistemiContext _context;

        public DoktorController(HastaneRandevuSistemiContext context)
        {
            _context = context;
        }

        // GET: Doktor
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("UserRole") != "Admin")
            {
                return RedirectToAction("Index","Home");
            }
            return _context.Doktor != null ? 
                          View(await _context.Doktor.ToListAsync()) :
                          Problem("Entity set 'HastaneRandevuSistemiContext.Doktor'  is null.");
        }

        // GET: Doktor/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (HttpContext.Session.GetString("UserRole") != "Admin")
            {
                return RedirectToAction("Index", "Home");
            }
            if (id == null || _context.Doktor == null)
            {
                return NotFound();
            }

            var doktor = await _context.Doktor
                .FirstOrDefaultAsync(m => m.Id == id);
            if (doktor == null)
            {
                return NotFound();
            }

            return View(doktor);
        }

        // GET: Doktor/Create
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("UserRole") != "Admin")
            {
                return RedirectToAction("Index", "Home");
            }
            ViewData["poliklinikId"] = new SelectList(_context.Set<Poliklinik>(), "Id", "PoliklinikIsmi");
            return View();
        }

        // POST: Doktor/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Isim,odaNo,poliklinikId,Sifre")] Doktor doktor)
        {
            if (HttpContext.Session.GetString("UserRole") != "Admin")
            {
                return RedirectToAction("Index", "Home");
            }
            _context.Add(doktor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            
        }

        // GET: Doktor/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (HttpContext.Session.GetString("UserRole") != "Admin")
            {
                return RedirectToAction("Index", "Home");
            }
            if (id == null || _context.Doktor == null)
            {
                return NotFound();
            }

            var doktor = await _context.Doktor.FindAsync(id);
            if (doktor == null)
            {
                return NotFound();
            }
            ViewData["poliklinikId"] = new SelectList(_context.Set<Poliklinik>(), "Id", "PoliklinikIsmi");
            return View(doktor);
        }

        // POST: Doktor/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Isim,odaNo,poliklinikId,Sifre")] Doktor doktor)
        {
            var Doktor = await _context.Doktor.FindAsync(id);
            doktor.Sifre = Doktor.Sifre;
            if (HttpContext.Session.GetString("UserRole") != "Admin")
            {
                return RedirectToAction("Index", "Home");
            }
            if (id != doktor.Id)
            {
                return NotFound();
            }
            
                    _context.Update(doktor);
                    await _context.SaveChangesAsync();
              
                return RedirectToAction(nameof(Index));

        }

        // GET: Doktor/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (HttpContext.Session.GetString("UserRole") != "Admin")
            {
                return RedirectToAction("Index", "Home");
            }
            if (id == null || _context.Doktor == null)
            {
                return NotFound();
            }

            var doktor = await _context.Doktor
                .FirstOrDefaultAsync(m => m.Id == id);
            if (doktor == null)
            {
                return NotFound();
            }

            return View(doktor);
        }

        // POST: Doktor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (HttpContext.Session.GetString("UserRole") != "Admin")
            {
                return RedirectToAction("Index", "Home");
            }
            if (_context.Doktor == null)
            {
                return Problem("Entity set 'HastaneRandevuSistemiContext.Doktor'  is null.");
            }
            var doktor = await _context.Doktor.FindAsync(id);
            if (doktor != null)
            {
                _context.Doktor.Remove(doktor);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DoktorExists(int id)
        {

          return (_context.Doktor?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        public IActionResult DoktorGiris()
        {
         
            return View();
        }
        public IActionResult GirisYap(Doktor doktor)
        {
            foreach (var user in _context.Doktor)
            {
                if (user.Id == doktor.Id && user.Sifre == doktor.Sifre)
                {
                    HttpContext.Session.SetString("SessionUser", doktor.odaNo);
                    var cookieOpt = new CookieOptions
                    {
                        Expires = DateTime.Now.AddMinutes(20)
                    };
                    HttpContext.Response.Cookies.Append("CookieID", user.odaNo, cookieOpt);
                    HttpContext.Session.SetString("UserRole", "Doktor");
                    return RedirectToAction("Index","Home");
                }
            }
            TempData["hata"] = "Kullanıcı adı veya şifre hatalı";
            return RedirectToAction("DoktorGiris");
        }
        public IActionResult GetDoktorIsim(int doktorId)
        {

            // Doktor ID'sine göre veritabanından doktoru bul
            var doktor = _context.Doktor.FirstOrDefault(d => d.Id == doktorId);

            if (doktor != null)
            {
                // Doktor varsa ismi JSON formatında döndür
                return Ok(new { DoktorIsim = doktor.Isim });
            }
            else
            {
                // Doktor bulunamazsa 404 Not Found döndür
                return NotFound();
            }
        }
    }
}
