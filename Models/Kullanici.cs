namespace BookStoreAPI.Models
{
    public class Kullanici
    {
        public int Id { get; set; }

        public required string Ad { get; set; }
        public required string Email { get; set; }
        public required string Sifre { get; set; }
        public required string Rol { get; set; }

        public ICollection<Siparis> Siparisler { get; set; } = new List<Siparis>();
        public ICollection<Favori> Favoriler { get; set; } = new List<Favori>();
    }
}
