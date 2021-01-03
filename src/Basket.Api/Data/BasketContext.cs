using Basket.Api.Data.Interfaces;
using StackExchange.Redis;

namespace Basket.Api.Data
{
    public class BasketContext : IBasketContext
    {
        public IDatabase Redis => throw new System.NotImplementedException();
    }
}
