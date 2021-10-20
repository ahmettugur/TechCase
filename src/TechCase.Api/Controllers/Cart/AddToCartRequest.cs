using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechCase.Api.Controllers
{
    public class AddToCartRequest
    {
        public Guid userId { get; set; }
        public List<CartItemRequest> cartItems { get; set; }
    }

    public class CartItemRequest
    {
        public long ProductId { get;  set; }

        public string Name { get;  set; }

        public decimal Price { get;  set; }
        public int StockQuantity { get;  set; }

    }
}
