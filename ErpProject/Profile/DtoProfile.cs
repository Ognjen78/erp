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
            CreateMap<Order, OrderConfirmDto>().ReverseMap();
            CreateMap<OrderItem, OrderItemConfirmDto>().ReverseMap();
            CreateMap<Product, ProductConfirmDto>().ReverseMap();
            CreateMap<ShippingAddress, ShippingConfirmDto>().ReverseMap();
        }
    }
}
