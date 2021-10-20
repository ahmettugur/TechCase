using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechCase.Application.Carts;
using TechCase.Application.Carts.AddToCart;
using TechCase.Domain.Carts;

namespace TechCase.Application.Common.Mappings
{
    public class CustomMapping : Profile
    {
        public CustomMapping()
        {
            CreateMap<AddToCartCommand, Cart>().ReverseMap();
            CreateMap<Cart, CartDto>().ReverseMap();
            CreateMap<CartItemDto, CartItem>().ReverseMap();
            CreateMap<AddToCartCommandCartItem, CartItem>().ReverseMap();
        }
    }
}
