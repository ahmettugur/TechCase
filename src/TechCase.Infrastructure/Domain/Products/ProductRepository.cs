using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TechCase.Domain.Products;
using TechCase.Domain.SeedWork;
using TechCase.Infrastructure.Database;
using TechCase.Infrastructure.Database.SqlServer;

namespace TechCase.Infrastructure.Domain.Products
{
    public class ProductRepository : IProductRepository
    {
        private readonly TechCaseContext _context;

        public ProductRepository(TechCaseContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<Product> GetByIdAsync(long id)
        {
            return await this._context.Products.SingleAsync(x => x.Id == id);
        }

        public async Task<List<Product>> GetProductsAsync(Expression<Func<Product, bool>> filter = null)
        {
            if (filter != null)
            {
               return await this._context.Products.Where(filter).ToListAsync();
            }

            return await this._context.Products.ToListAsync();
        }
    }
}
