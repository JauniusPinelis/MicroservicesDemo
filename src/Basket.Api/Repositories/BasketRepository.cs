using Basket.Api.Entities;
using Basket.Api.Repositories.Interfaces;
using System.Threading.Tasks;

namespace Basket.Api.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        public Task<BasketCart> DeleteBasket(string username)
        {
            throw new System.NotImplementedException();
        }

        public Task<BasketCart> GetBasket(string userName)
        {
            throw new System.NotImplementedException();
        }

        public Task<BasketCart> UpdateBasket(BasketCart basket)
        {
            throw new System.NotImplementedException();
        }
    }
}
