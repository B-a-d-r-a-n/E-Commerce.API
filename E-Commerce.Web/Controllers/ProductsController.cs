//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;

//namespace E_Commerce.Web.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class ProductsController : ControllerBase
//    {
//        [HttpGet("{id}")]
//        public ActionResult<Product> Get(int id) // baseUrl/api/products/10
//        {
//            return new Product() { Id = id, Name = "Name" };
//        }
//        [HttpGet]
//        public ActionResult<IEnumerable<Product>> GetAll() // baseUrl/api/products
//        {
//            return new List<Product>([new Product() { Id= 10 , Name = "Name"}]); 
//        }
//        [HttpDelete] // not specified in attribute = query unless special command given in method [from]
//        public ActionResult<Product> Delete(int id) // baseUrl/api/products?id=10
//        {

//            return new Product() { Id = id, Name = "Name" };

//        }
//        [HttpPost]
//        public ActionResult<Product> Create(Product product) // Verb HttpPost // baseUrl/api/Products
//        {
//            return new Product() { Id = product.Id, Name = product.Name };
//        }

//        [HttpPut]
//        public ActionResult<Product> Update(Product product) // Verb HttpPut // baseUrl/api/Products
//        {
//            var obj = new ProblemDetails();
//            return new Product() { Id = product.Id, Name = product.Name };
//        }



//    }
//   public class Product
//        {
//            public int Id { get; set; }
//            public string Name { get; set; }

//        }

//}