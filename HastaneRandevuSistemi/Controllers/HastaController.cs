using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HastaneRandevuSistemi.Data;
using HastaneRandevuSistemi.Models;
using Microsoft.AspNetCore.Http;

namespace HastaneRandevuSistemi.Controllers
{
    public class HastaController : Controller
    {
        private readonly HastaneRandevuSistemiContext _context;

        public HastaController(HastaneRandevuSistemiContext context)
        {
            _context = context;
        }

        // GET: Hasta
        public async Task<IActionResult> Index()
        {
              return _context.Hasta != null ? 
                          View(await _context.Hasta.ToListAsync()) :
                          Problem("Entity set 'HastaneRandevuSistemiContext.Hasta'  is null.");
        }

        // GET: Hasta/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Hasta == null)
            {
                return NotFound();
            }

            var hasta = await _context.Hasta
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hasta == null)
            {
                return NotFound();
            }

            return View(hasta);
        }

        // GET: Hasta/Create
        public IActionResult KayitOl()
        {
            return View();
        }

        // POST: Hasta/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Isim,Sifre,KimlikNo,DogumTarihi")] Hasta hasta)
        {
            if (HttpContext.Session.GetString("SessionUser") is null)
            { 
                if (ModelState.IsValid)
                {
                    foreach (var user in _context.Hasta)
                    {
                        if (user.KimlikNo == hasta.KimlikNo)
                        {
                            TempData["hata"] = "Bu kişi zaten kayıtlı";
                            return View();
                        }
                    }
                        _context.Add(hasta);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(hasta);
            }
            else
                return RedirectToAction("Index");
        }

        // GET: Hasta/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Hasta == null)
            {
                return NotFound();
            }

            var hasta = await _context.Hasta.FindAsync(id);
            if (hasta == null)
            {
                return NotFound();
            }
            return View(hasta);
        }

        // POST: Hasta/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Sifre,Isim,KimlikNo,DogumTarihi")] Hasta hasta)
        {
            if (id != hasta.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hasta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HastaExists(hasta.Id))
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
            return View(hasta);
        }

        // GET: Hasta/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (HttpContext.Session.GetString("UserRole") != "Admin")
            {
                return RedirectToAction("Index", "Home");
            }
            if (id == null || _context.Hasta == null)
            {
                return NotFound();
            }

            var hasta = await _context.Hasta
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hasta == null)
            {
                return NotFound();
            }

            return View(hasta);
        }

        // POST: Hasta/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (HttpContext.Session.GetString("UserRole") != "Admin")
            {
                return RedirectToAction("Index", "Home");
            }
            if (_context.Hasta == null)
            {
                return Problem("Entity set 'HastaneRandevuSistemiContext.Hasta'  is null.");
            }
            var hasta = await _context.Hasta.FindAsync(id);
            if (hasta != null)
            {
                _context.Hasta.Remove(hasta);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HastaExists(int id)
        {
          return (_context.Hasta?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        public IActionResult Giris()
        {
            if (HttpContext.Session.GetString("SessionUser") is null)
                return View();
            else
                return RedirectToAction("Index");
        }
        public IActionResult GirisYap(Hasta hasta)
        {
            foreach(var user in _context.Hasta)
            {
                if(user.KimlikNo==hasta.KimlikNo && user.Sifre==hasta.Sifre)
                {
                    HttpContext.Session.SetString("SessionUser", hasta.KimlikNo);
                    HttpContext.Session.SetInt32("SessionId", user.Id);
                    var cookieOpt = new CookieOptions
                    {
                        Expires = DateTime.Now.AddMinutes(20)
                    };
                    HttpContext.Response.Cookies.Append("CookieTC",user.KimlikNo,cookieOpt);
                    HttpContext.Session.SetString("UserRole", "Hasta");
                    return RedirectToAction("Index","Home");
                }
                
            }
            TempData["hata"] = "Kullanıcı adı veya şifre hatalı";
            return RedirectToAction("Giris");
        }
        public IActionResult CikisYap()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index","Home");
        }

    }
}
