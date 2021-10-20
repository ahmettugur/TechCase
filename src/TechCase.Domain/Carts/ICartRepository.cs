using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechCase.Domain.Carts
{
    public interface ICartRepository
    {
        Task<Cart> Get(string key);
        Task<Cart> AddOrUpdateCart(Cart cart);
        Task<bool> Remove(string key);
    }
}
