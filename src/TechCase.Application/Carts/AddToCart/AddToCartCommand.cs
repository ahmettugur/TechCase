using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechCase.Application.Common.Results;
using TechCase.Application.Configuration.Commands;

namespace TechCase.Application.Carts.AddToCart
{
    public class AddToCartCommand : CommandBase<IDataResult<CartDto>>
    {
        public Guid UserId { get; set; }
        public List<AddToCartCommandCartItem> cartItems { get; set; }
        public decimal TotalPrice { get; set; }
    }

    public class AddToCartCommandCartItem
    {
        public long ProductId { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }
        public int StockQuantity { get; set; }

    }
}
