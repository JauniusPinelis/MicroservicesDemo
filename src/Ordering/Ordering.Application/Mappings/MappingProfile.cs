using AutoMapper;
using EventBusRabbitMq.Events;
using Ordering.Application.Commands;
using Ordering.Application.Responses;
using Ordering.Core.Entities;

namespace Ordering.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BacketCheckoutEvent, CheckoutOrderCommand>().ReverseMap();
            CreateMap<OrderResponse, Order>().ReverseMap();
            CreateMap<Order, CheckoutOrderCommand>().ReverseMap();
        }
    }
}
