
using Domain.Models.Basket;
using Shared.DataTransferObjects.Basket;

namespace Services.MappingProfiles
{
    internal class BasketProfile:Profile
    {
        public BasketProfile()
        {
            CreateMap<CustomerBasket,BasketDTO>().ReverseMap();
            CreateMap<BasketItem,BasketItemDTO>().ReverseMap();
        }
    }
}
