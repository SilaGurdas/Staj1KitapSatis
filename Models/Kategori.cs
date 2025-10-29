using System;
using System.Collections.Generic;

namespace BookStoreAPI.Models

{
    public class Kategori
    {
        public int Id { get; set; }

        public required string Ad { get; set; }

        public ICollection<Kitap> Kitaplar { get; set; } = new List<Kitap>(); 
    }
}

