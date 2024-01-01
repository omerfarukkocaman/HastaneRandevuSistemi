using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HastaneRandevuSistemi.Data;
using HastaneRandevuSistemi.Models;

namespace HastaneRandevuSistemi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RandevuApiController : ControllerBase
    {
        private readonly HastaneRandevuSistemiContext _context;

        public RandevuApiController(HastaneRandevuSistemiContext context)
        {
            _context = context;
        }

        // GET: api/RandevuApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Randevu>>> GetRandevu()
        {
          if (_context.Randevu == null)
          {
              return NotFound();
          }
            return await _context.Randevu.ToListAsync();
        }

        // GET: api/RandevuApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Randevu>> GetRandevu(int id)
        {
          if (_context.Randevu == null)
          {
              return NotFound();
          }
            var randevu = await _context.Randevu.FindAsync(id);

            if (randevu == null)
            {
                return NotFound();
            }

            return randevu;
        }

        // PUT: api/RandevuApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRandevu(int id, Randevu randevu)
        {
            if (id != randevu.Id)
            {
                return BadRequest();
            }

            _context.Entry(randevu).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RandevuExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/RandevuApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Randevu>> PostRandevu(Randevu randevu)
        {
          if (_context.Randevu == null)
          {
              return Problem("Entity set 'HastaneRandevuSistemiContext.Randevu'  is null.");
          }
            _context.Randevu.Add(randevu);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRandevu", new { id = randevu.Id }, randevu);
        }

        // DELETE: api/RandevuApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRandevu(int id)
        {
            if (_context.Randevu == null)
            {
                return NotFound();
            }
            var randevu = await _context.Randevu.FindAsync(id);
            if (randevu == null)
            {
                return NotFound();
            }

            _context.Randevu.Remove(randevu);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RandevuExists(int id)
        {
            return (_context.Randevu?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
