using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SushiMarket.DAL.Entities;
using SushiMarket.DAL.Entities.Location;
using SushiMarket.DAL.Entities.NewsItem;
using SushiMarket.DAL.Entities.Users;

namespace SushiMarket.DAL
{
    public class SushiMarketDbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Promotion> Promotions { get; set; } = null!;
        public DbSet<NewsItem> News { get; set; } = null!;
        public DbSet<Location> Locations { get; set; } = null!;

        public DbSet<RefreshToken> RefreshTokens { get; set; } = null!;

        public SushiMarketDbContext(DbContextOptions<SushiMarketDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.HasDefaultSchema("auth");

            builder.Entity<RefreshToken>(entity =>
            {
                entity.ToTable("RefreshTokens", "auth");
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.TokenHash).IsUnique();
                entity.Property(e => e.TokenHash).IsRequired();
                entity.Property(e => e.UserId).IsRequired();
                entity.Property(e => e.Created).IsRequired();
                entity.Property(e => e.Expires).IsRequired();
                entity.HasIndex(e => e.UserId);
            });

            builder.Entity<User>(entity =>
            {
                entity.Property(u => u.UserName).HasMaxLength(256);
                entity.ToTable("Users", "auth");
            });

            builder.Entity<IdentityRole<int>>(entity => entity.ToTable("Roles", "auth"));
            builder.Entity<IdentityUserRole<int>>(entity => entity.ToTable("UserRoles", "auth"));
            builder.Entity<IdentityUserClaim<int>>(entity => entity.ToTable("UserClaims", "auth"));
            builder.Entity<IdentityUserLogin<int>>(entity => entity.ToTable("UserLogins", "auth"));
            builder.Entity<IdentityRoleClaim<int>>(entity => entity.ToTable("RoleClaims", "auth"));
            builder.Entity<IdentityUserToken<int>>(entity => entity.ToTable("UserTokens", "auth"));

            builder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}