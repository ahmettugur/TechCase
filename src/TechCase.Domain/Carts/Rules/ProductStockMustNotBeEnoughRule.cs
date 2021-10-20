using System.Collections.Generic;
using TechCase.Domain.Products.Interfaces;
using TechCase.Domain.SeedWork;

namespace TechCase.Domain.Carts.Rules
{
    public class ProductStockMustNotBeEnoughRule: IBusinessRule
    {
        private readonly IProductStockChecker _productStockChecker;
        private readonly List<CartItem> _cartItems;
        private string message = "";

        public ProductStockMustNotBeEnoughRule(IProductStockChecker productStockChecker, List<CartItem> cartItems)
        {
            _productStockChecker = productStockChecker;
            _cartItems = cartItems;
        }

        public bool IsBroken()
        {
            var isBroken = _productStockChecker.StockControl(_cartItems);
            message = _productStockChecker.Message;
            return isBroken;
        }

        public string Message => message;
    }
}