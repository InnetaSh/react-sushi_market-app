using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SushiMarket.DAL;

namespace SushiMarket.BLL.Services.Services
{
    public class TokenCleanupService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public TokenCleanupService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await DoWork();

                try
                {
                    await Task.Delay(TimeSpan.FromHours(24), stoppingToken);
                }
                catch (OperationCanceledException)
                {
                    break;
                }
            }
        }

        private async Task DoWork()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<SushiMarketDbContext>();

                var expiredTokens = context.RefreshTokens
                    .Where(t => t.Expires < DateTime.UtcNow);

                if (await expiredTokens.AnyAsync())
                {
                    context.RefreshTokens.RemoveRange(expiredTokens);
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}