using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechCase.Application.Carts.AddToCart
{
    public class AddToCartCommandValidator: AbstractValidator<AddToCartCommand>
    {
        public AddToCartCommandValidator()
        {
            RuleFor(_=>_.UserId).NotEmpty().WithMessage("UserId cannot be empty");
            RuleFor(_ => _.cartItems).NotNull().WithMessage("CartItems cannot be null");
            RuleForEach(x => x.cartItems).ChildRules(cartItem => {
                cartItem.RuleFor(x => x.Price).GreaterThan(0).WithMessage($"Price cannot be zero."); ;
                cartItem.RuleFor(x => x.Name).NotEmpty().WithMessage($"Name cannot be emtpty");
                cartItem.RuleFor(x => x.StockQuantity).GreaterThan(0).WithMessage($"StockQuantity  cannot be zero."); ;
                cartItem.RuleFor(x => x.ProductId).GreaterThan(0).WithMessage($"Product Id cannot be zero.");
            });
        }
    }
}
