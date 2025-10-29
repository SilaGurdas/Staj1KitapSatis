namespace BookStoreAPI.Models
{
        public class Favori
        {
            public int Id { get; set; }

            public int KullaniciId { get; set; }
            public Kullanici? Kullanici { get; set; }  

            public int KitapId { get; set; }
            public Kitap? Kitap { get; set; }          
        }
    }
