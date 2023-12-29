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
    public class HastaneController : Controller
    {
        private readonly HastaneRandevuSistemiContext _context;

        public HastaneController(HastaneRandevuSistemiContext context)
        {
            _context = context;
        }

        // GET: Hastane
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("UserRole") != "Admin")
            {
                return RedirectToAction("Index", "Home");
            }
            var hastaneRandevuSistemiContext = _context.Hastane.Include(h => h.ilce);
            return View(await hastaneRandevuSistemiContext.ToListAsync());
        }

        // GET: Hastane/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (HttpContext.Session.GetString("UserRole") != "Admin")
            {
                return RedirectToAction("Index", "Home");
            }
            if (id == null || _context.Hastane == null)
            {
                return NotFound();
            }

            var hastane = await _context.Hastane
                .Include(h => h.ilce)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hastane == null)
            {
                return NotFound();
            }

            return View(hastane);
        }

        // GET: Hastane/Create
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("UserRole") != "Admin")
            {
                return RedirectToAction("Index", "Home");
            }
            ViewData["IlceId"] = new SelectList(_context.Set<Ilce>(), "Id", "Id");
            return View();
        }

        // POST: Hastane/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,HastaneIsmi,IlceId")] Hastane hastane)
        {
            if (HttpContext.Session.GetString("UserRole") != "Admin")
            {
                return RedirectToAction("Index", "Home");
            }
            if (ModelState.IsValid)
            {
                _context.Add(hastane);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IlceId"] = new SelectList(_context.Set<Ilce>(), "Id", "Id", hastane.IlceId);
            return View(hastane);
        }

        // GET: Hastane/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (HttpContext.Session.GetString("UserRole") != "Admin")
            {
                return RedirectToAction("Index", "Home");
            }
            if (id == null || _context.Hastane == null)
            {
                return NotFound();
            }

            var hastane = await _context.Hastane.FindAsync(id);
            if (hastane == null)
            {
                return NotFound();
            }
            ViewData["IlceId"] = new SelectList(_context.Set<Ilce>(), "Id", "Id", hastane.IlceId);
            return View(hastane);
        }

        // POST: Hastane/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,HastaneIsmi,IlceId")] Hastane hastane)
        {
            if (HttpContext.Session.GetString("UserRole") != "Admin")
            {
                return RedirectToAction("Index", "Home");
            }
            if (id != hastane.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hastane);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HastaneExists(hastane.Id))
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
            ViewData["IlceId"] = new SelectList(_context.Set<Ilce>(), "Id", "Id", hastane.IlceId);
            return View(hastane);
        }

        // GET: Hastane/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (HttpContext.Session.GetString("UserRole") != "Admin")
            {
                return RedirectToAction("Index", "Home");
            }
            if (id == null || _context.Hastane == null)
            {
                return NotFound();
            }

            var hastane = await _context.Hastane
                .Include(h => h.ilce)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hastane == null)
            {
                return NotFound();
            }

            return View(hastane);
        }

        // POST: Hastane/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (HttpContext.Session.GetString("UserRole") != "Admin")
            {
                return RedirectToAction("Index", "Home");
            }
            if (_context.Hastane == null)
            {
                return Problem("Entity set 'HastaneRandevuSistemiContext.Hastane'  is null.");
            }
            var hastane = await _context.Hastane.FindAsync(id);
            if (hastane != null)
            {
                _context.Hastane.Remove(hastane);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HastaneExists(int id)
        {
          return (_context.Hastane?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
