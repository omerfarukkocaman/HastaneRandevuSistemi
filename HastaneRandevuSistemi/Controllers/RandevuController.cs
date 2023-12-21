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
            var hastaneRandevuSistemiContext = _context.Randevu.Include(r => r.Hasta).Include(r => r.doktor);
            return View(await hastaneRandevuSistemiContext.ToListAsync());
        }

        // GET: Randevu/Details/5
        public async Task<IActionResult> Details(int? id)
        {
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
            ViewData["HastaId"] = new SelectList(_context.Hasta, "Id", "Isim");
            ViewData["DoktorId"] = new SelectList(_context.Doktor, "Id", "Isim");
            return View();
        }

        // POST: Randevu/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,HastaId,RandevuTarihi,DoktorId")] Randevu randevu)
        {
            if (ModelState.IsValid)
            {
                _context.Add(randevu);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["HastaId"] = new SelectList(_context.Hasta, "Id", "Isim", randevu.HastaId);
            ViewData["DoktorId"] = new SelectList(_context.Doktor, "Id", "Isim", randevu.DoktorId);
            return View(randevu);
        }

        // GET: Randevu/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
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
        public IActionResult GetAvailableAppointments(DateTime selectedDate)
        {
            // Belirli bir tarihe ait uygun randevu saatlerini çekme
            var availableHours = _context.Randevu
                .Where(r => r.RandevuTarihi.Date == selectedDate.Date)
                .Select(r => r.RandevuTarihi.Hour)
                .ToList();

            return Json(availableHours);
        }
    }
}
