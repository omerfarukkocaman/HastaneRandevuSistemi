using System.ComponentModel;

namespace HastaneRandevuSistemi.Models
{
    public class Sehir
    {
        public int Id { get; set; }
        [DisplayName("Şehir")]
        public string SehirIsmi { get; set; }
        public List<Ilce>? ilceler {  get; set; }
    }
}
