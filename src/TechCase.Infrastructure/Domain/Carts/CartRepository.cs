using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TechCase.Domain.Carts;
using TechCase.Infrastructure.Database.Redis;

namespace TechCase.Infrastructure.Domain.Carts
{
    public class CartRepository : ICartRepository
    {
        private readonly RedisConnection _redisConnection;

        public CartRepository(RedisConnection redisConnection)
        {
            _redisConnection = redisConnection;
        }

        public async Task<Cart> AddOrUpdateCart(Cart cart)
        {
            await _redisConnection.GetDb().StringSetAsync(cart.userId.ToString(), JsonSerializer.Serialize(cart));

            return await Get(cart.userId.ToString());
        }
        public async Task<Cart> Get(string key)
        {
            var existBasket = await _redisConnection.GetDb().StringGetAsync(key);
            return JsonSerializer.Deserialize<Cart>(existBasket);
        }

        public async Task<bool> Remove(string key)
        {
            return await _redisConnection.GetDb().KeyDeleteAsync(key);
        }
    }
}
