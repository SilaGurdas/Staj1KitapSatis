using BookStoreAPI.Models;
using Microsoft.EntityFrameworkCore;


namespace BookStoreAPI.Data
{
    public class UygulamaDbContext : DbContext
    {
        public UygulamaDbContext(DbContextOptions<UygulamaDbContext> options) : base(options)
        {
        }

        public DbSet<Kullanici> Kullanicilar { get; set; }
        public DbSet<Kategori> Kategoriler { get; set; }
        public DbSet<Kitap> Kitaplar { get; set; }
        public DbSet<Siparis> Siparisler { get; set; }
        public DbSet<Favori> Favoriler { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Kullanici>().ToTable("Kullanicilar");
            modelBuilder.Entity<Kategori>().ToTable("Kategoriler");
            modelBuilder.Entity<Kitap>(entity =>
            {
                entity.ToTable("Kitaplar");

                entity.Property(k => k.Fiyat)
                      .HasColumnType("decimal(18,2)");
            });
            modelBuilder.Entity<Siparis>().ToTable("Siparisler");
            modelBuilder.Entity<Favori>().ToTable("Favoriler");
        }
    }
}
 