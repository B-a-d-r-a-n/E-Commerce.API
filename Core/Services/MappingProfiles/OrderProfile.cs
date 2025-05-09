
using Domain.Models.OrderModels;
using Microsoft.Extensions.Configuration;
using Shared.Orders;

namespace Services.MappingProfiles
{
    internal class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<OrderAddress,AddressDTO>().ReverseMap();
            CreateMap<OrderItem, OrderItemDTO>()
                .ForMember(d => d.ProductName, option =>
                    option.MapFrom(s => s.Product.ProductName))
                .ForMember(d => d.PictureUrl, option =>
                option.MapFrom<OrderItemPictureUrlResolver>());
            CreateMap<Order, OrderResponse>()
                .ForMember(d => d.DeliveryMethod, options =>
                options.MapFrom(s => s.DeliveryMethod.ShortName))
                .ForMember(d => d.Total, options =>
                options.MapFrom(s => s.DeliveryMethod.Price + s.Subtotal));
            CreateMap<DeliveryMethod, DeliveryMethodResponse>();


        }
    }
    internal class OrderItemPictureUrlResolver(IConfiguration configuration)
        : IValueResolver<OrderItem, OrderItemDTO, string>
    {
        public string Resolve(OrderItem source, OrderItemDTO destination, string destMember, ResolutionContext context)
        {
            return string.IsNullOrWhiteSpace(source.Product.PictureUrl) ? string.Empty 
                : $"{configuration["BaseUrl"]}{source.Product.PictureUrl}";
        }
    }
}
