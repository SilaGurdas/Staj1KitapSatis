namespace BookStoreMVC.Models
{
    public class Kategori
    {
        public int Id { get; set; }
        public required string Ad { get; set; }

        public List<Kitap>? Kitaplar { get; set; }
    }
}
