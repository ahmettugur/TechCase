using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechCase.Domain.Carts
{
    public class CartItem
    {
        public long ProductId { get;  set; }

        public string Name { get;  set; }

        public decimal Price { get;  set; }
        public int StockQuantity { get;  set; }

    }
}
