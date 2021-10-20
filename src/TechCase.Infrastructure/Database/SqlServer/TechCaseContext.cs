using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TechCase.Domain.Products;
using TechCase.Domain.SeedWork;
using TechCase.Infrastructure.Domain.Products;

namespace TechCase.Infrastructure.Database.SqlServer
{
    public class TechCaseContext: DbContext, IUnitOfWork
    {
        public DbSet<Product> Products { get; set; }

        public TechCaseContext(DbContextOptions<TechCaseContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductEntityTypeConfiguration());
        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            await base.SaveChangesAsync(cancellationToken);
            return true;
        }
    }

    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<TechCaseContext>
    {
        public TechCaseContext CreateDbContext(string[] args)
        {
            var connectionString = Environment.GetEnvironmentVariable("TechCaseConnection");
            var builder = new DbContextOptionsBuilder<TechCaseContext>();
            if (string.IsNullOrEmpty(connectionString))
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();
                connectionString = configuration.GetConnectionString("TechCaseConnection");
            }
            builder.UseSqlServer(connectionString);
            return new TechCaseContext(builder.Options);
        }
    }
}
