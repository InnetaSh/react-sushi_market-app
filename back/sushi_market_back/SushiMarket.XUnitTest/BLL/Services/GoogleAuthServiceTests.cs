using Microsoft.Extensions.Configuration;
using SushiMarket.BLL.Services;
using Xunit;

namespace SushiMarket.Tests.Services
{
    public class GoogleAuthServiceTests
    {
        [Fact]
        public async Task ValidateTokenAsync_ShouldReturnNull_WhenTokenIsInvalid()
        {
            var myConfiguration = new Dictionary<string, string>
        {
            {"GoogleAuth:ClientId", "fake-client-id"}
        };

            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(myConfiguration!)
                .Build();

            var service = new GoogleAuthService(configuration);

            var result = await service.ValidateTokenAsync("invalid-token-string");

            Assert.Null(result);
        }
    }
}
