using System;

namespace BookStoreMVC.Models
{
    public class Siparis
    {
        public int Id { get; set; }
        public int KullaniciId { get; set; }
        public int KitapId { get; set; }
        public int Adet { get; set; }
        public DateTime Tarih { get; set; }
    }
}
