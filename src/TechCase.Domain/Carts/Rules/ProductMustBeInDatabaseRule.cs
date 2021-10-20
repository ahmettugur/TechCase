using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechCase.Domain.Products;
using TechCase.Domain.Products.Interfaces;
using TechCase.Domain.SeedWork;

namespace TechCase.Domain.Carts.Rules
{
    internal class ProductMustBeInDatabaseRule : IBusinessRule
    {
        private readonly IProductIsExistChecker _productIsExistChecker;
        private readonly List<CartItem> _cartItems;
        private string message = "";

        public ProductMustBeInDatabaseRule(IProductIsExistChecker productIsExistChecker, List<CartItem> cartItems)
        {
            _productIsExistChecker = productIsExistChecker;
            _cartItems = cartItems;
        }

        public string Message => message;

        public bool IsBroken()
        {
            var idsToSelect = _cartItems.Select(id => id.ProductId).ToList();
            var isBroken = _productIsExistChecker.IsExist(idsToSelect);
            message = _productIsExistChecker.Message;

            return isBroken;
        }
    }
}
