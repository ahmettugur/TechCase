using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechCase.Domain.Products;
using TechCase.Domain.Products.Interfaces;

namespace TechCase.Application.Products.DomainService
{
    public class ProductIsExistChecker : IProductIsExistChecker
    {
        private readonly IProductRepository _productRepository;

        public ProductIsExistChecker(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public string Message { get; private set; }

        public bool IsExist(List<long> ids)
        {
            var stringBuilder = new StringBuilder();
            var isBroken = false;
            var products = _productRepository.GetProductsAsync(x => ids.Contains(x.Id)).GetAwaiter().GetResult();
            foreach (var item in ids.Where(item => products.All(_ => _.Id != item)))
            {
                stringBuilder.AppendLine($"Product cannot found. ProductId: {item}");
                isBroken = true;
            }

            Message = stringBuilder.ToString();
            return isBroken;
        }
    }
}
