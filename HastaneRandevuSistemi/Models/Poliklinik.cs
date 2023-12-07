using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HastaneRandevuSistemi.Models
{
    public class Poliklinik
    {
        public int Id { get; set; }
        public string PoliklinikIsmi { get; set; }
        public List<Klinik> klinikler {  get; set; }
    }
}