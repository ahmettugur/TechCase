using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechCase.Domain.SeedWork;

namespace TechCase.Domain.Carts.Rules
{
    internal class ProductQuantityLessThanEqualsZeroRule : IBusinessRule
    {
        public string Message => _message;
        private string _message;
        private readonly List<CartItem> _cartItems;

        public ProductQuantityLessThanEqualsZeroRule(List<CartItem> cartItems)
        {
            _cartItems = cartItems;
        }

        public bool IsBroken()
        {
            var stringBuilder = new StringBuilder();
            var isBroken = false;
            foreach (var item in _cartItems.Where(item => item.StockQuantity <= 0))
            {
                stringBuilder.AppendLine($"Product cannot found. ProductId: {item}");
                isBroken = true;
            }
            _message = stringBuilder.ToString();
            return isBroken;
        }
    }
}
