﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HastaneRandevuSistemi.Models
{
    public class Hasta
    {
		[Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Ad soyad giriniz")]
        [DisplayName("Ad Soyad")]
        [RegularExpression("^[a-zA-ZğüşıöçĞÜŞİÖÇ\\s]+$", ErrorMessage = "Geçerli bir isim giriniz.")]
        public string Isim { get; set; }
        [DataType(DataType.Password)]
        [StringLength(16, MinimumLength = 8, ErrorMessage = "Metin 8 ile 16 karakter arasında olmalıdır.")]
        [Required(ErrorMessage = "Şifre giriniz")]
        [DisplayName("Şifre")]
        public string Sifre { get; set; }
        [Required(ErrorMessage = "TC Kimlik Numarası giriniz")]
        [DisplayName("TC Kimlik Numarası")]
        [StringLength(11)]
        [RegularExpression("^[0-9]+$", ErrorMessage = "TC Kimlik Numarası sadece sayılardan oluşmalıdır.")]
        public string KimlikNo { get; set; }
        [Required(ErrorMessage = "Doğum tarihi giriniz")]
        [AfterCurrentDate(ErrorMessage = "Geçerli bir tarih seçiniz.")]
        [DisplayName("Doğum Tarihiniz")]
        [DataType(DataType.Date)]
        public DateTime DogumTarihi { get; set; }
        public List<Randevu>? randevular { get; set; }
    }
}