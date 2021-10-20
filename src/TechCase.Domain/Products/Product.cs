using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechCase.Domain.SeedWork;

namespace TechCase.Domain.Products
{
    public class Product : Entity
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
    }
}
