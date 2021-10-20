using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using TechCase.Application.Configuration.Settings;
using TechCase.Infrastructure.Database.Redis;
using Microsoft.Extensions.Options;
using TechCase.Domain.Carts;
using TechCase.Infrastructure.Domain.Carts;
using TechCase.Application.Products.DomainService;
using TechCase.Domain.Products.Interfaces;
using TechCase.Application.Common.Behaviours;
using FluentValidation;

namespace TechCase.Application
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddSingleton<RedisConnection>(sp =>
            {
                var redisSettings = sp.GetRequiredService<IOptions<RedisSettings>>().Value;

                var redis = new RedisConnection(redisSettings.Host, redisSettings.Port);

                redis.Connect();

                return redis;
            });

            services.AddScoped<ICartRepository, CartRepository>();
            services.AddScoped<IProductIsExistChecker, ProductIsExistChecker>();
            services.AddScoped<IProductStockChecker, ProductStockChecker>();


            return services;
        }
    }
}
