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
        [ForeignKey("Id")]
        public int hastaId { get; set; }
		public Hasta hasta { get; set; }
		[Required]
        [CustomTimeRange(480, 1020)]
        [DisplayName("Randevu Tarihi")]
        public DateTime RandevuTarihi { get; set; }
        [ForeignKey("Id")]
        [DisplayName("Doktor")]
        public int doktorId { get; set; }
		public Doktor doktor { get; set; }
	}
}