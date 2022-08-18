using ECommerceAPI.Application.Repositories.Products;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase {
	private readonly IProductWriteRepository _productWriteRepository;
	private readonly IProductReadRepository _productReadRepository;

	public ProductsController(IProductWriteRepository productWriteRepository, IProductReadRepository productReadRepository) {
		this._productWriteRepository = productWriteRepository;
		this._productReadRepository = productReadRepository;
	}

	[HttpGet]
	public async Task<IActionResult> GetAll() {
		List<Domain.Entities.Product> x = new() {
			new() {
				Id = Guid.NewGuid(),
				Name = "Product 1",
				Price = 100,
				Stock = 10,
				CreatedDate = DateTime.UtcNow,
				UpdatedDate = DateTime.UtcNow
			},
			new() {
				Id = Guid.NewGuid(),
				Name = "Product 2",
				Price = 400,
				Stock = 2,
				CreatedDate = DateTime.UtcNow,
				UpdatedDate = DateTime.UtcNow
			},
			new() {
				Id = Guid.NewGuid(),
				Name = "Product 3",
				Price = 700,
				Stock = 18,
				CreatedDate = DateTime.UtcNow,
				UpdatedDate = DateTime.UtcNow
			},
		};

		await _productWriteRepository.AddRangeAsync(x);
		await _productWriteRepository.SaveAsync();
		return Ok(await _productReadRepository.GetAll());
	}

	[HttpGet("{id}")]
	public async Task<IActionResult> GetById(Guid id) {
		return Ok(await _productReadRepository.GetByIdAsync(id));
	}
}