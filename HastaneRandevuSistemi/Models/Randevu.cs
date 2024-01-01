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
    public class Randevu
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Hasta")]
        public int HastaId { get; set; }
        public Hasta Hasta { get; set; }

        [Required]
        [CustomTimeRange(480, 1020)]
        [DisplayName("Randevu Tarihi")]
        public DateTime RandevuTarihi { get; set; }

        [ForeignKey("doktor")]
        [DisplayName("Doktor")]
        public int DoktorId { get; set; }
        [DisplayName("Doktor")]
        public Doktor doktor { get; set; }
    }
}