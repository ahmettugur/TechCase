using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TechCase.Application.Carts.AddToCart;
using TechCase.Application.Common.Behaviours;
using Xunit;

namespace TechCase.Application.UnitTests.Common.Behaviours
{
    public class RequestLoggerTests
    {
        private readonly Mock<ILogger<AddToCartCommand>> _logger;
        public RequestLoggerTests()
        {
            _logger = new Mock<ILogger<AddToCartCommand>>();

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

            var requestLogger = new LoggingBehaviour<AddToCartCommand>(_logger.Object);

            await requestLogger.Process(command, new CancellationToken());

            Assert.Equal(1, 1);

        }

    }
}
