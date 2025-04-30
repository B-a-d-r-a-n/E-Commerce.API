
using Microsoft.AspNetCore.Identity;

namespace Services
{
    public class ServiceManager(IUnitOfWork unitOfWork,
        IMapper mapper,IBasketRepository basketRepository, UserManager<ApplicationUser> userManager) 
        : IServiceManager
    {
        private readonly Lazy<IProductService> _lazyProductService
            =new Lazy<IProductService>(()=> new ProductService(unitOfWork,mapper));
        private readonly Lazy<IBasketService> _lazyBasketService
          = new Lazy<IBasketService>(() => new BasketService(basketRepository, mapper));
        private readonly Lazy<IAuthenticationService> _lazyAuthenticationService
  = new Lazy<IAuthenticationService>(() => new AuthenticationService(userManager));
        public IProductService ProductService
            => _lazyProductService.Value;

        public IBasketService BasketService => _lazyBasketService.Value;

        public IAuthenticationService AuthenticationService => _lazyAuthenticationService.Value;
    }
}
