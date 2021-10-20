using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Respawn;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TechCase.Api;
using TechCase.Application.Carts;
using TechCase.Application.Carts.AddToCart;
using TechCase.Application.Common.Exception;
using TechCase.Application.Common.Results;
using TechCase.Domain.Carts;
using TechCase.Domain.SeedWork;
using TechCase.Infrastructure.Database.SqlServer;
using Xunit;

namespace TechCase.Application.IntegrationTests.Carts.AddToCart
{
    public class AddToCartTests
    {
        private static IConfigurationRoot _configuration;
        private static IServiceScopeFactory _scopeFactory;
        private static Checkpoint _checkpoint;

        public AddToCartTests()
        {

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .AddEnvironmentVariables();

            _configuration = builder.Build();

            var startup = new Startup(_configuration);

            var services = new ServiceCollection();

            services.AddSingleton(Mock.Of<IWebHostEnvironment>(w =>
                w.EnvironmentName == "Development" &&
                w.ApplicationName == "TechCase.Api"));

            services.AddLogging();

            startup.ConfigureServices(services);


            _scopeFactory = services.BuildServiceProvider().GetService<IServiceScopeFactory>();

            _checkpoint = new Checkpoint
            {
                TablesToIgnore = new[] { "__EFMigrationsHistory" }
            };

            EnsureDatabase();
        }



        [Fact]
        public async Task AddToCart_EmptyCommand_ReturnValidationException()
        {
            var command = new AddToCartCommand();
            await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<ValidationException>();

        }

        [Fact]
        public async Task AddToCart_ValidCommand_ReturnResult()
        {
            var command = new AddToCartCommand()
            {
                TotalPrice = 20,
                UserId = Guid.NewGuid(),
                cartItems = new List<AddToCartCommandCartItem>
                {
                    new AddToCartCommandCartItem
                    {
                        Name =  "Test 1",
                        Price = 15,
                        ProductId = 1,
                        StockQuantity = 10
                    }
                }
            };
            var cart = await SendAsync(command);
            cart.Data.Should().NotBeNull();
            cart.Code.Should().Be(200);
            cart.Should().BeAssignableTo(typeof(ApiDataResult<>));
        }

        [Fact]
        public async Task AddToCart_InValidProductId_ReturnBusinessRuleValidationException()
        {
            var command = new AddToCartCommand()
            {
                TotalPrice = 20,
                UserId = Guid.NewGuid(),
                cartItems = new List<AddToCartCommandCartItem>
                {
                    new AddToCartCommandCartItem
                    {
                        Name =  "Test 1",
                        Price = 15,
                        ProductId = 10,
                        StockQuantity = 10
                    }
                }
            };
            await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<BusinessRuleValidationException>();
        }

        [Fact]
        public async Task AddToCart_EmtptyCommandCartline_ReturnValidationException()
        {
            var command = new AddToCartCommand()
            {
                TotalPrice = 20,
                UserId = Guid.NewGuid()
            };
            //IDataResult<CartDto>
            await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<ValidationException>();

        }

        #region
        private static void EnsureDatabase()
        {
            using var scope = _scopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetService<TechCaseContext>();

            context.Database.Migrate();
        }
        public static async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
        {
            using var scope = _scopeFactory.CreateScope();

            var mediator = scope.ServiceProvider.GetService<ISender>();

            return await mediator.Send(request);
        }
        #endregion
    }
}
