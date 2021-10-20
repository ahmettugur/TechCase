using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechCase.Domain.Carts;
using TechCase.Domain.Products;
using TechCase.Domain.Products.Interfaces;

namespace TechCase.Application.Products.DomainService
{
    public class ProductStockChecker: IProductStockChecker
    {
        private readonly IProductRepository _productRepository;
        public string Message { get; private set; }

        public ProductStockChecker(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }


        public bool StockControl(List<CartItem> cartItems)
        {
            var ids = cartItems.Select(id => id.ProductId).ToList();
            var stringBuilder = new StringBuilder();
            var isBroken = false;
            var products = _productRepository.GetProductsAsync(x => ids.Contains(x.Id)).GetAwaiter().GetResult();

            foreach (var product in products.Where(product => product.StockQuantity < cartItems.FirstOrDefault(_=>_.ProductId == product.Id).StockQuantity))
            {
                stringBuilder.AppendLine($"ProductId: {product.Id} stock not enough.");
                isBroken = true;
            }
            
            Message = stringBuilder.ToString();
            return isBroken;
        }
    }
}