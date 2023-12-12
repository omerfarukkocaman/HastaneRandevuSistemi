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
    public class Doktor
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        [DisplayName("Ad Soyad")]
        public string Isim { get; set; }
		[Required]
        [DisplayName("Oda Numarası")]
        public int odaNo { get; set; }
		[DataType(DataType.Password)]
		[StringLength(16, MinimumLength = 8, ErrorMessage = "Metin 8 ile 16 karakter arasında olmalıdır.")]
		[Required]
        [DisplayName("Şifre")]
        public string Sifre { get; set; }
	}
}