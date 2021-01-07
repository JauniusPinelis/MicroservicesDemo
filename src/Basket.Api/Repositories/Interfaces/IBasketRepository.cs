using Basket.Api.Entities;
using System.Threading.Tasks;

namespace Basket.Api.Repositories.Interfaces
{
    public interface IBasketRepository
    {
        public Task<BasketCart> GetBasket(string userName);
        public Task<BasketCart> UpdateBasket(BasketCart basket);
        public Task<bool> DeleteBasket(string username);
    }
}
