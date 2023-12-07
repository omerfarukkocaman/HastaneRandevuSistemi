using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HastaneRandevuSistemi.Models
{
    public class Klinik
    {
        public int OdaId { get; set; }
        public string OdaIsmi { get; set; }
        public Doktor doktor { get; set; }
        public Poliklinik poliklinik { get; set; }
    }
}