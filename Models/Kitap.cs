namespace BookStoreMVC.Models
{
    public class Kitap
    {
        public int Id { get; set; }
        public string Ad { get; set; } = null!;
        public string Yazar { get; set; } = null!;
        public decimal Fiyat { get; set; }
        public string? ResimUrl { get; set; }
        public int KategoriId { get; set; }

        public Kategori? Kategori { get; set; }
    }
}
