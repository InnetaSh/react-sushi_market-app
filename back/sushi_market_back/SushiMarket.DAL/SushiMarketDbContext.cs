using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SushiMarket.DAL.Entities;
using SushiMarket.DAL.Entities.Users;

namespace SushiMarket.DAL
{
    public class SushiMarketDbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<Product> Products { get; set; } = null!;

        public DbSet<Promotion> Promotions { get; set; } = null!;

        public SushiMarketDbContext(DbContextOptions<SushiMarketDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}