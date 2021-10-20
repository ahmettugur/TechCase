using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechCase.Application.Carts.AddToCart;

namespace TechCase.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CartController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart([FromBody] AddToCartRequest addToCartRequest)
        {
            var command = new AddToCartCommand
            {
                UserId = addToCartRequest.userId,
                TotalPrice = addToCartRequest.cartItems.Sum(_ => _.Price * _.StockQuantity),
                cartItems = addToCartRequest.cartItems.Select(_ => new AddToCartCommandCartItem
                {
                    ProductId = _.ProductId,
                    Name = _.Name,
                    StockQuantity = _.StockQuantity,
                    Price = _.Price
                }).ToList()
            };

            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
