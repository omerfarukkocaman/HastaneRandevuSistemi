using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HastaneRandevuSistemi.Models
{
    public class Poliklinik
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DisplayName("Poliklinik")]
        public string PoliklinikIsmi { get; set; }
        [ForeignKey("hastane")]
        public int HastaneId { get; set; }
        public Hastane hastane { get; set; }

        public List<Doktor>? Doktorlar { get; set; }
    }
}