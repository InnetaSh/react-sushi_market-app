using Microsoft.EntityFrameworkCore;
using SushiMarket.DAL.Entities;

namespace SushiMarket.DAL
{
    public class SushiMarketDbContext : DbContext
    {
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Product> Products => Set<Product>();

        public SushiMarketDbContext(DbContextOptions<SushiMarketDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasPrecision(18, 2);
        }
    }
}