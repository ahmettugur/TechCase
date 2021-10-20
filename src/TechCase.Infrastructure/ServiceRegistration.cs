using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechCase.Domain.Products;
using TechCase.Domain.SeedWork;
using TechCase.Infrastructure.Database.SqlServer;
using TechCase.Infrastructure.Domain.Products;

namespace TechCase.Infrastructure
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddPersistenceRegistration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<TechCaseContext>(opt =>
            {
                opt.UseSqlServer(configuration.GetConnectionString("TechCaseConnection"));
                opt.EnableSensitiveDataLogging();
            });

            services.AddScoped<IProductRepository, ProductRepository>();

            var optionsBuilder = new DbContextOptionsBuilder<TechCaseContext>()
                .UseSqlServer(configuration.GetConnectionString("TechCaseConnection"));


            using var dbContext = new TechCaseContext(optionsBuilder.Options);
            dbContext.Database.EnsureCreated();
            dbContext.Database.Migrate();

            return services;
        }
    }
}
