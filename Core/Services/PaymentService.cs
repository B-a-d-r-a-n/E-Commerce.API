

global using Product = Domain.Models.Products.Product;
global using Microsoft.Extensions.Configuration;
using Stripe;
namespace Services
{
    internal class PaymentService(IBasketRepository basketRepository
        ,IUnitOfWork unitOfWork
        ,IConfiguration configuration,
        IMapper mapper)
        : IPaymentService
    {
        public async Task<BasketDTO> CreateOrUpdatePaymentIntent(string basketId)
        {
            //StripeConfiguration.ApiKey = configuration["Stripe:SecretKey"];
            StripeConfiguration.ApiKey = configuration.GetRequiredSection("Stripe")["SecretKey"];  
            //basketrepo => get basket 
            var basket = await basketRepository.GetAsync(basketId) ??
                throw new BasketNotFoundException(basketId);
            var productRepo = unitOfWork.GetRepository<Product>();
            foreach(var item in basket.Items)
            {
                var product = await productRepo.GetAsync(item.Id) ??
                    throw new ProductNotFoundException(item.Id);
                item.Price = product.Price;
            }
            ArgumentNullException.ThrowIfNull(basket.DeliveryMethodId);
            var method = await unitOfWork.GetRepository<DeliveryMethod>().GetAsync(basket.DeliveryMethodId.Value) ??
                throw new DeliveryMethodNotFoundException(basket.DeliveryMethodId.Value);

            basket.ShippingPrice = method.Cost;

            var amount = (long)(basket.Items.Sum(item => item.Price * item.Quantity) + method.Cost)*100;

            //create || update 
            var service = new PaymentIntentService();
            if (string.IsNullOrEmpty(basket.PaymentIntentId))
            {
                //create
                var options = new PaymentIntentCreateOptions
                {
                    Amount = amount,
                    Currency = "AED",
                    PaymentMethodTypes = ["card"] // new List<string> { "card" }
                    ,
                    Metadata = new Dictionary<string, string>
                    {
                        { "basketId", basket.Id }
                    }
                };
               
                var intent = await service.CreateAsync(options);
                basket.PaymentIntentId = intent.Id;
                basket.ClientSecret = intent.ClientSecret;
            }
            else
            {
                //update
                var options = new PaymentIntentUpdateOptions
                {
                    Amount = amount,
                    //Currency = "AED",
                    //Metadata = new Dictionary<string, string>
                    //{
                    //    { "basketId", basket.Id }
                    //}
                };
                await service.UpdateAsync(basket.PaymentIntentId, options);
              //nafs el intentID nafs el clientSecret mesh me7tag a3mel assign
            }
            await basketRepository.UpdateAsync(basket);
            return mapper.Map<BasketDTO>(basket);
        }
    }
}
