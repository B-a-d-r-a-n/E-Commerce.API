
using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects.Basket
{
    public record BasketItemDTO
    {
        public int Id { get; init; } // product id == int
        public string ProductName { get; init; } = default!;
        public string PictureUrl { get; init; } = default!;
        [Range(1,double.MaxValue)]
        public decimal Price { get; init; }
        [Range(1, 99)]

        public int Quantity { get; init; } 
    }

    }
