﻿
namespace Domain.Models.Basket
{
    public class BasketItem
    {
        public int Id { get; set; } //product id == int
        public string ProductName { get; set; } = default!;
        public string PictureUrl { get; set; } = default!;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
