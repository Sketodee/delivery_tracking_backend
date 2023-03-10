using AutoMapper;
using DeliveryTracking.Dtos;

namespace DeliveryTracking
{
    public class AutomapperProfile : Profile 
    {
        public AutomapperProfile()
        {
            CreateMap<Order, AddOrderDto>(); 
            CreateMap<AddOrderDto, Order>();


            CreateMap<Item, AddItemDto>(); 
            CreateMap<AddItemDto, Item>(); 

        }
    }
}
