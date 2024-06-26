using AutoMapper;
using ErpProject.DTO;
using ErpProject.Models;

namespace ErpProject.Profile
{
    public class DtoProfile : AutoMapper.Profile
    {

        public DtoProfile() 
        {
            CreateMap<User, UserConfirmDto>().ReverseMap();
            CreateMap<Admin, AdminConfirmDto>().ReverseMap();
            CreateMap<Order, OrderConfirmDto>().ReverseMap().ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems));
            CreateMap<OrderItem, OrderItemConfirmDto>().ReverseMap();
            CreateMap<OrderItem, OrderItemDto>().ReverseMap();
            CreateMap<Product, ProductConfirmDto>().ReverseMap();
            CreateMap<ShippingAddress, ShippingConfirmDto>().ReverseMap();
        }
    }
}
