using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HastaneRandevuSistemi.Models
{
    public class Doktor
    {
        public int Id { get; set; }
        public string Isim { get; set; }
        public Klinik klinik { get; set; }
    }
}