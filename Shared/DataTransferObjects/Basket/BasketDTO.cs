namespace Shared.DataTransferObjects.Basket
{
    public record BasketDTO
    {
        public string Id { get; init; }

        public IEnumerable<BasketItemDTO> Items { get; set; }

        public string? PaymentIntentId { get; set; }
        public string? ClientSecret { get; set; }
        public int? DeliveryMethodId { get; set; }
        public decimal ShippingPrice { get; set; }
    }

    }
