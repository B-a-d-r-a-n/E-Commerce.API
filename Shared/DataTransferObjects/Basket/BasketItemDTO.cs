
using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects.Basket
{
    public record BasketItemDTO
    {
        public int Id { get; init; } // product id == int
        public string ProductName { get; init; }
        public string PictureUrl { get; init; }
        [Range(1, short.MaxValue)]
        public decimal Price { get; init; }
        [Range(1, short.MaxValue)]
        public int Quantity { get; init; }
    }

    }
