using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechCase.Application.Carts.AddToCart
{
    public class AddToCartCommandValidator : AbstractValidator<AddToCartCommand>
    {
        public AddToCartCommandValidator()
        {
            RuleFor(_ => _.UserId).NotEmpty().WithMessage("UserId cannot be empty");
            RuleFor(_ => _.cartItems).Must(CartItemsIsNotNullOrGreaterThanZero).WithMessage("CartItems cannot be null");
            RuleForEach(x => x.cartItems).ChildRules(cartItem => {
                cartItem.RuleFor(x => x.Price).GreaterThan(0).WithMessage($"Price cannot be zero."); ;
                cartItem.RuleFor(x => x.Name).NotEmpty().WithMessage($"Name cannot be emtpty");
                cartItem.RuleFor(x => x.StockQuantity).GreaterThan(0).WithMessage($"StockQuantity  cannot be zero."); ;
                cartItem.RuleFor(x => x.ProductId).GreaterThan(0).WithMessage($"Product Id cannot be zero.");
            });
        }

        private bool CartItemsIsNotNullOrGreaterThanZero(List<AddToCartCommandCartItem> cartItems)
        {
            if (cartItems == null || cartItems.Count == 0)
            {
                return false;
            }

            return true;
        }
    }
}


//            RuleFor(_ => _.cartItems).NotNull().WithMessage("CartItems cannot be null");
//When(_ => _.cartItems.Count > 0, () =>
//{
//    RuleForEach(x => x.cartItems).ChildRules(cartItem =>
//    {
//        cartItem.RuleFor(x => x.Price).GreaterThan(0).WithMessage($"Price cannot be zero.");
//        cartItem.RuleFor(x => x.Name).NotEmpty().WithMessage($"Name cannot be emtpty");
//        cartItem.RuleFor(x => x.StockQuantity).GreaterThan(0).WithMessage($"StockQuantity  cannot be zero."); ;
//        cartItem.RuleFor(x => x.ProductId).GreaterThan(0).WithMessage($"Product Id cannot be zero.");
//    });
//}).Otherwise(() =>
//{
//    RuleFor(_ => _.cartItems.Count).Equal(0).WithMessage($"cartItems cannot be zero.");
//});
//RuleFor(_ => _.cartItems).NotNull()
//    .WithMessage("CartItems cannot be null")
//    .Must((_, cartItems) => cartItems?.Count <= 0)
//    .WithMessage(@"CartItems count connot be zero");

//When(_ => _.cartItems != null, () => { })
//.Otherwise(() =>
//{
//    RuleFor(_ => _.cartItems.Count).GreaterThan(0).WithMessage("CartItems count connot be zero");
//});
//RuleForEach(x => x.cartItems).ChildRules(cartItem =>
//{
//    cartItem.RuleFor(x => x.Price).GreaterThan(0).WithMessage($"Price cannot be zero."); ;
//    cartItem.RuleFor(x => x.Name).NotEmpty().WithMessage($"Name cannot be emtpty");
//    cartItem.RuleFor(x => x.StockQuantity).GreaterThan(0).WithMessage($"StockQuantity  cannot be zero."); ;
//    cartItem.RuleFor(x => x.ProductId).GreaterThan(0).WithMessage($"Product Id cannot be zero.");
//});