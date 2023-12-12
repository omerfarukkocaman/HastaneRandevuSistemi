using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HastaneRandevuSistemi.Models;

namespace HastaneRandevuSistemi.Data
{
    public class HastaneRandevuSistemiContext : DbContext
    {
        public HastaneRandevuSistemiContext (DbContextOptions<HastaneRandevuSistemiContext> options)
            : base(options)
        {
        }
        public DbSet<HastaneRandevuSistemi.Models.Hasta>? Hasta { get; set; }
        public DbSet<HastaneRandevuSistemi.Models.Randevu>? Randevu { get; set; }
        public DbSet<HastaneRandevuSistemi.Models.Doktor>? Doktor { get; set; }

    }
}
