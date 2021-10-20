using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechCase.Application.Carts
{
    public class CartDto
    {
        public Guid userId { get; set; }
        public List<CartItemDto> cartItems { get; set; }
        public decimal TotalPrice { get; set; }
    }

    public class CartItemDto
    {
        public long ProductId { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }
        public int StockQuantity { get; set; }

    }
}
