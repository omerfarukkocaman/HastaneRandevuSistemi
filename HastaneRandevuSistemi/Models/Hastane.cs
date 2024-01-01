using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HastaneRandevuSistemi.Models
{
    public class Hastane
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Hastane")]
        public string HastaneIsmi { get; set; }
        public List<Poliklinik>? poliklinikler { get; set; }
        [ForeignKey("ilce")]
        [DisplayName("Ilçe")]
        public int IlceId { get; set; }
        public Ilce ilce { get; set; }
    }
}
