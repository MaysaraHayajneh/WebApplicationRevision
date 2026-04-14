using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplicationRevision.Models;

namespace WebApplicationRevision.Controllers
{
	[Route("api/[controller]")]
	public class ProductController : ControllerBase
	{
		private readonly AppDbContext appDbContext;

		public ProductController(AppDbContext appDbContext)
		{
			this.appDbContext = appDbContext;
		}

		[HttpGet]
		[Route("")]
		public IActionResult GetAll()
		{
			IEnumerable<Product> products = appDbContext.Set<Product>().ToList();

			return Ok(products);
		}
		[Authorize]
		[HttpGet]
		[Route("{id:int}")]
		public IActionResult Get(int id, [FromHeader(Name = "Accept-Language")] string lang)
		{
			Product products = appDbContext.Set<Product>().Where(x => x.Id == id).FirstOrDefault();

			return Ok(products);
		}
		[HttpPost]
		[Route("")]
		public IActionResult Get(Product product)
		{
			var newProduct = new Product()
			{
				Price = product.Price,
				Name = product.Name,
				Quantity = product.Quantity,
			};

			var id = appDbContext.Set<Product>().Add(newProduct);

			appDbContext.SaveChanges();

			return Created();
		}


	}
}
