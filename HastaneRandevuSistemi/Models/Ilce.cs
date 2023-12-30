using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HastaneRandevuSistemi.Models
{
    public class Ilce
    {
        [Key]
        public int Id { get; set; }
        public string IlceIsmi { get; set; }
        [ForeignKey("sehir")]
        public int SehirId { get; set; }
        public Sehir sehir { get; set; }
        public List<Hastane>? hastaneler { get; set; }
    }
}
