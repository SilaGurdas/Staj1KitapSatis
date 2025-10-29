namespace BookStoreMVC.Models
{
    public class FavoriItem
    {
        public int KitapId { get; set; }
        public string KitapAdi { get; set; }
        public decimal Fiyat { get; set; }
        public string? ResimUrl { get; set; }
    }
}
