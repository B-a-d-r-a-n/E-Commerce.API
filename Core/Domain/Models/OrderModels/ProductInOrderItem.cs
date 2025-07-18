﻿
namespace Domain.Models.OrderModels
{
    public class ProductInOrderItem
    {
        public ProductInOrderItem()
        {
            
        }
        public ProductInOrderItem(int productId, string productName, string pictureUrl)
        {
            ProductId = productId;
            ProductName = productName;
            PictureUrl = pictureUrl;
        }

        public int ProductId { get; set; } //product id == int
        public string ProductName { get; set; } = default!;
        public string PictureUrl { get; set; } = default!;
    }
}
