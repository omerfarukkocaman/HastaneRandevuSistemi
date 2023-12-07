using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HastaneRandevuSistemi.Models
{
    public class Randevu
    {
        public int Id { get; set; }
        public Hasta? hasta { get; set; }
        public DateTime RandevuTarihi {  get; set; }
        public Doktor doktor { get; set; }
        public bool RandevuDurumu {  get; set; }
    }
}