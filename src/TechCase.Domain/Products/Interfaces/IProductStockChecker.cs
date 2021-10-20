using System.Collections.Generic;
using TechCase.Domain.Carts;

namespace TechCase.Domain.Products.Interfaces
{
    public interface IProductStockChecker: IChecker
    {
        bool StockControl(List<CartItem> cartItems);
    }
}