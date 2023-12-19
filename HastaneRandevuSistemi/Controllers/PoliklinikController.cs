﻿using System;
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
    public class PoliklinikController : Controller
    {
        private readonly HastaneRandevuSistemiContext _context;

        public PoliklinikController(HastaneRandevuSistemiContext context)
        {
            _context = context;
        }

        // GET: Poliklinik
        public async Task<IActionResult> Index()
        {
            var hastaneRandevuSistemiContext = _context.Poliklinik.Include(p => p.hastane);
            return View(await hastaneRandevuSistemiContext.ToListAsync());
        }

        // GET: Poliklinik/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Poliklinik == null)
            {
                return NotFound();
            }

            var poliklinik = await _context.Poliklinik
                .Include(p => p.hastane)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (poliklinik == null)
            {
                return NotFound();
            }

            return View(poliklinik);
        }

        // GET: Poliklinik/Create
        public IActionResult Create()
        {
            ViewData["HastaneId"] = new SelectList(_context.Set<Hastane>(), "Id", "Id");
            return View();
        }

        // POST: Poliklinik/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PoliklinikIsmi,HastaneId")] Poliklinik poliklinik)
        {
            if (ModelState.IsValid)
            {
                _context.Add(poliklinik);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["HastaneId"] = new SelectList(_context.Set<Hastane>(), "Id", "Id", poliklinik.HastaneId);
            return View(poliklinik);
        }

        // GET: Poliklinik/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Poliklinik == null)
            {
                return NotFound();
            }

            var poliklinik = await _context.Poliklinik.FindAsync(id);
            if (poliklinik == null)
            {
                return NotFound();
            }
            ViewData["HastaneId"] = new SelectList(_context.Set<Hastane>(), "Id", "Id", poliklinik.HastaneId);
            return View(poliklinik);
        }

        // POST: Poliklinik/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PoliklinikIsmi,HastaneId")] Poliklinik poliklinik)
        {
            if (id != poliklinik.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(poliklinik);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PoliklinikExists(poliklinik.Id))
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
            ViewData["HastaneId"] = new SelectList(_context.Set<Hastane>(), "Id", "Id", poliklinik.HastaneId);
            return View(poliklinik);
        }

        // GET: Poliklinik/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Poliklinik == null)
            {
                return NotFound();
            }

            var poliklinik = await _context.Poliklinik
                .Include(p => p.hastane)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (poliklinik == null)
            {
                return NotFound();
            }

            return View(poliklinik);
        }

        // POST: Poliklinik/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Poliklinik == null)
            {
                return Problem("Entity set 'HastaneRandevuSistemiContext.Poliklinik'  is null.");
            }
            var poliklinik = await _context.Poliklinik.FindAsync(id);
            if (poliklinik != null)
            {
                _context.Poliklinik.Remove(poliklinik);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PoliklinikExists(int id)
        {
          return (_context.Poliklinik?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
