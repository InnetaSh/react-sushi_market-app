using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SushiMarket.BLL.Helpers;
using SushiMarket.BLL.MediatR.Behaviors;
using SushiMarket.BLL.Services;
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

            services.AddAutoMapper(cfg =>
            {
                cfg.AddMaps(Assembly.GetExecutingAssembly());
            });

            return services;
        }
    }
}