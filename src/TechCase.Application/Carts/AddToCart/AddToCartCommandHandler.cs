using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TechCase.Application.Common.Results;
using TechCase.Application.Configuration.Commands;
using TechCase.Domain.Carts;
using TechCase.Domain.Products.Interfaces;

namespace TechCase.Application.Carts.AddToCart
{
    public class AddToCartCommandHandler : ICommandHandler<AddToCartCommand, IDataResult<CartDto>>
    {
        private readonly IMapper _mapper;
        private readonly ICartRepository _cartRepository;
        private readonly IProductIsExistChecker _productIsExistChecker;
        private readonly IProductStockChecker _productStockChecker;

        public AddToCartCommandHandler(ICartRepository cartRepository, IMapper mapper, IProductIsExistChecker productIsExistChecker, IProductStockChecker productStockChecker)
        {
            _cartRepository = cartRepository;
            _mapper = mapper;
            _productIsExistChecker = productIsExistChecker;
            _productStockChecker = productStockChecker;
        }

        public async Task<IDataResult<CartDto>> Handle(AddToCartCommand request, CancellationToken cancellationToken)
        {
            var cartItems = _mapper.Map<List<CartItem>>(request.cartItems);
            var cart = Cart.CreateRegistered(_productIsExistChecker,_productStockChecker,request.UserId, cartItems, request.TotalPrice);
            var result = await _cartRepository.AddOrUpdateCart(cart);
            var cartDto = _mapper.Map<CartDto>(result);
            return new ApiDataResult<CartDto>(200,cartDto);
        }
    }
}
