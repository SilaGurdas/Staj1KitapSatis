namespace BookStoreMVC.Models
{
    public class SepetItem
    {
        public int KitapId { get; set; }
        public string KitapAdi { get; set; } = null!;
        public decimal Fiyat { get; set; }
        public int Adet { get; set; }
        public string? ResimUrl { get; set; }
    }
}
