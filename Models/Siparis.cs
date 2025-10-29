
using System;
using BookStoreAPI.Models; 

namespace BookStoreAPI.Models
{
    public class Siparis
    {
        public int Id { get; set; }

        public int KullaniciId { get; set; }
        public Kullanici? Kullanici { get; set; }  

        public int KitapId { get; set; }
        public Kitap? Kitap { get; set; }  

        public int Adet { get; set; }
        public DateTime Tarih { get; set; }
    }
}

