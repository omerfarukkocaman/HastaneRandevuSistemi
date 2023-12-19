namespace HastaneRandevuSistemi.Models
{
    public class Sehir
    {
        public int Id { get; set; }
        public string SehirIsmi { get; set; }
        public List<Ilce>? ilceler {  get; set; }
    }
}
