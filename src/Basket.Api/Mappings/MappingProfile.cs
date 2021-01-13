using AutoMapper;
using Basket.Api.Entities;
using EventBusRabbitMq.Events;

namespace Basket.Api.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BasketCheckout, BacketCheckoutEvent>().ReverseMap();
        }
    }
}
