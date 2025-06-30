
using Domain.Models.OrderModels;
using Shared.Orders;

namespace Services
{
    internal class OrderService(IMapper mapper,IUnitOfWork _unitOfWork,IBasketRepository _basketRepository)
        : IOrderService
    {

        public async Task<OrderResponse> CreateAsync(OrderRequest request, string email)
        {
            var basket = await _basketRepository.GetAsync(request.BasketId)??
                throw new BasketNotFoundException(request.BasketId);

            ArgumentNullException.ThrowIfNull(basket.PaymentIntentId);

            var orderRepo = _unitOfWork.GetRepository<Order, Guid>();

            var existingOrder = await orderRepo
                .GetAsync(new OrderWithPaymentIntentSpecification(basket.PaymentIntentId));
            
            if(existingOrder != null) orderRepo.Delete(existingOrder);

            var address = mapper.Map<OrderAddress>(request.ShipToAddress);

            List<OrderItem> items = [];
            var productRepo = _unitOfWork.GetRepository<Product>();
            foreach (var item in basket.Items)
            {
                var product = await productRepo.GetAsync(item.Id)??
                    throw new ProductNotFoundException(item.Id);

                items.Add(CreateOrderItem(product, item));

                item.Price = product.Price;
            }
            var method = await _unitOfWork.GetRepository<DeliveryMethod>()
                .GetAsync(request.DeliveryMethodId) ??
                throw new DeliveryMethodNotFoundException(request.DeliveryMethodId);

            var subtotal = items.Sum(item => item.Price * item.Quantity);


            var order = new Order(email, items, address, method, subtotal
                ,basket.PaymentIntentId);

            orderRepo.Add(order);

            await _unitOfWork.SaveChangesAsync();

            return mapper.Map<OrderResponse>(order);
        }

        private static OrderItem CreateOrderItem(Product product, BasketItem basketItem)
            => new(new(product.Id,product.Name,product.PictureUrl),
                product.Price,basketItem.Quantity);

        public async Task<IEnumerable<OrderResponse>> GetAllAsync(string email)
        {
            var orders = await _unitOfWork.GetRepository<Order, Guid>()
                .GetAllAsync(new OrderSpecifications(email));
            return mapper.Map<IEnumerable<OrderResponse>>(orders);
        }

        public async Task<OrderResponse> GetAsync(Guid id)
        {
            var order = await _unitOfWork.GetRepository<Order, Guid>()
    .GetAsync(new OrderSpecifications(id));
            return mapper.Map<OrderResponse>(order);
        }

        public async Task<IEnumerable<DeliveryMethodResponse>> GetDeliveryMethodsAsync()
        {
            var deliveryMethods = await _unitOfWork.GetRepository<DeliveryMethod>().GetAllAsync();
            return mapper.Map<IEnumerable<DeliveryMethodResponse>>(deliveryMethods);
        }
    }
}
