namespace BookStoreAPI.Models
{
    public class Kitap
    {
        public int Id { get; set; }

        public required string Ad { get; set; }
        public required string Yazar { get; set; }
        public decimal Fiyat { get; set; }

        public string? ResimUrl { get; set; }
        public int KategoriId { get; set; }

        public Kategori? Kategori { get; set; }
    }
}

