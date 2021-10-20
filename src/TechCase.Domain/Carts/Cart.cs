using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechCase.Domain.Carts.Rules;
using TechCase.Domain.Products.Interfaces;
using TechCase.Domain.SeedWork;

namespace TechCase.Domain.Carts
{
    public class Cart
    {
        public Guid userId { get; private set; }
        public List<CartItem> cartItems { get; private set; }
        public decimal TotalPrice { get; private set; }

        public Cart(Guid userId, List<CartItem> cartItems, decimal totalPrice)
        {
            this.userId = userId;
            this.cartItems = cartItems;
            TotalPrice = totalPrice;
        }

        public static Cart CreateRegistered(IProductIsExistChecker productIsExistChecker,IProductStockChecker productStockChecker, Guid userId, List<CartItem> cartItems,decimal totalPrice)
        {
            CheckRule(new ProductMustBeInDatabaseRule(productIsExistChecker, cartItems));
            CheckRule(new ProductQuantityLessThanEqualsZeroRule(cartItems));
            CheckRule(new ProductStockMustNotBeEnoughRule(productStockChecker,cartItems));
            
            return new Cart(userId, cartItems, totalPrice);
        }

        protected static void CheckRule(IBusinessRule rule)
        {
            if (rule.IsBroken())
            {
                throw new BusinessRuleValidationException(rule);
            }
        }
    }
}
