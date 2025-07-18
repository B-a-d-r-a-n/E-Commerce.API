﻿
namespace ServicesAbstraction
{
    public interface IServiceManager
    {
        public IProductService ProductService { get;}
        public IBasketService BasketService { get;}
        public IAuthenticationService AuthenticationService { get;}
        public IOrderService OrderService { get;}
        IPaymentService PaymentService { get; }
    }
}
