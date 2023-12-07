using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HastaneRandevuSistemi.Models
{
    public class Hasta
    {
        public int Id { get; set; }
        public string Isim { get; set; }
        public string KimlikNo { get; set; }
        public DateTime DogumTarihi { get; set; }
        public List<Randevu> randevular { get; set; }
    }
}