using BookStoreAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

public class UygulamaDbContextFactory : IDesignTimeDbContextFactory<UygulamaDbContext>
{
    public UygulamaDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<UygulamaDbContext>();
        optionsBuilder.UseSqlServer("Server=SILAG\\SQLEXPRESS;Database=BookStoreDb;Trusted_Connection=True;TrustServerCertificate=True;Encrypt=False;");

        return new UygulamaDbContext(optionsBuilder.Options);
    }
}
