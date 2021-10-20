using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TechCase.Domain.Products
{
    public interface IProductRepository
    {
        Task<Product> GetByIdAsync(long id);
        Task<List<Product>> GetProductsAsync(Expression<Func<Product, bool>> filter = null);
    }
}
