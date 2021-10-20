using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechCase.Domain.Products;

namespace TechCase.Infrastructure.Database.SqlServer
{
    public class TechCaseContextSeed
    {
        public async Task SeedAsync(TechCaseContext context, ILogger<TechCaseContext> logger)
        {
            var policy = CreatePolicy(logger, nameof(OrderDbContextSeed));

            await policy.ExecuteAsync(async () =>
            {
                using (context)
                {
                    context.Database.Migrate();

                    var products = new List<Product>
                    {
                        new Product {Name = "Macbook Pro",Price = 1150, StockQuantity = 20},
                        new Product {Name = "IPhone 11",Price = 1050, StockQuantity = 20},
                        new Product {Name = "Macbook Air",Price = 1150, StockQuantity = 20}
                    };

                    if (!context.Products.Any())
                    {
                        context.Products.AddRange(products);

                        await context.SaveChangesAsync();
                    }

                }
            });
        }

        private object OrderDbContextSeed()
        {
            throw new NotImplementedException();
        }

        private AsyncRetryPolicy CreatePolicy(ILogger<TechCaseContext> logger, string prefix, int retries = 10)
        {
            return Policy.Handle<SqlException>().
                WaitAndRetryAsync(
                    retryCount: retries,
                    sleepDurationProvider: retry => TimeSpan.FromSeconds(5),
                    onRetry: (exception, timeSpan, retry, ctx) =>
                    {
                        logger.LogWarning(exception, "[{prefix}] Exception {ExceptionType} with message {Message} detected on attempt {retry} of {retries}", prefix, exception.GetType().Name, exception.Message, retry, retries);
                    }
                );
        }
    }
}
