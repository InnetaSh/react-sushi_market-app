using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SushiMarket.BLL.Services.Services;
using SushiMarket.DAL;
using SushiMarket.DAL.Entities.Users;
using System.Reflection;

namespace SushiMarket.Tests.Services
{
    public class TokenCleanupServiceTests
    {
        private static SushiMarketDbContext CreateDb(string dbName)
        {
            var options = new DbContextOptionsBuilder<SushiMarketDbContext>()
                .UseInMemoryDatabase(dbName)
                .Options;

            return new SushiMarketDbContext(options);
        }

        [Fact]
        public async Task DoWork_ShouldRemoveExpiredTokens()
        {
            var dbName = Guid.NewGuid().ToString();

            await using (var context = CreateDb(dbName))
            {
                context.RefreshTokens.AddRange(
                    new RefreshToken
                    {
                        TokenHash = "1",
                        Expires = DateTime.UtcNow.AddDays(-1)
                    },
                    new RefreshToken
                    {
                        TokenHash = "2",
                        Expires = DateTime.UtcNow.AddDays(1)
                    });

                await context.SaveChangesAsync();
            }

            var services = new ServiceCollection();
            services.AddDbContext<SushiMarketDbContext>(o =>
                o.UseInMemoryDatabase(dbName));

            var provider = services.BuildServiceProvider();

            var service = new TokenCleanupService(provider);

            var method = typeof(TokenCleanupService)
                .GetMethod("DoWork", BindingFlags.NonPublic | BindingFlags.Instance);

            await (Task)method!.Invoke(service, null)!;

            await using var verifyContext = CreateDb(dbName);

            var remaining = await verifyContext.RefreshTokens.ToListAsync();

            remaining.Should().HaveCount(1);
            remaining.First().TokenHash.Should().Be("2");
        }
    }
}