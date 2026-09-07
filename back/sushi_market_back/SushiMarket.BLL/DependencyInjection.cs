using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using SushiMarket.BLL.Helpers;
using SushiMarket.BLL.MediatR.Behaviors;
using SushiMarket.BLL.Services;
using SushiMarket.BLL.Services.Interfaces.Cloudinary;
using SushiMarket.BLL.Services.Interfaces.Logging;
using SushiMarket.BLL.Services.Interfaces.Users;
using SushiMarket.BLL.Services.Services;
using System.Reflection;

namespace SushiMarket.BLL
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddBll(this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
            });

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddScoped<TranslatorHelper.Translator>();

            services.AddScoped<ICloudinaryService, CloudinaryService>();

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IJwtTokenService, JwtTokenService>();
            services.AddScoped<IRefreshTokenService, RefreshTokenService>();
            services.AddScoped<IGoogleAuthService, GoogleAuthService>();

            services.AddScoped<ILoggerService, LoggerService>();

            services.AddHostedService<TokenCleanupService>();

            services.AddAutoMapper(cfg =>
            {
                cfg.AddMaps(Assembly.GetExecutingAssembly());
            });

            return services;
        }
    }
}