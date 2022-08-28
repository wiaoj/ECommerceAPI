using ECommerceAPI.Application.Repositories.Products;
using ECommerceAPI.Application.ViewModels.Products;
using ECommerceAPI.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Linq;
using System.Linq.Expressions;
using ECommerceAPI.Application.RequestParamaters;

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
	public async Task<IActionResult> GetAll([FromQuery] Pagination pagination) {
		//var x = new List<Product>();
		//for(int i = 1; i <= 500; i++) {
		//	x.Add(new() {
		//		Name = $"Product {i}",
		//		Price = new Random().Next(i,i*500),
		//		Stock = (Int16)(i * 500 > Int16.MaxValue ? Int16.MaxValue : i * 500)
		//	});
		//}
		//await _productWriteRepository.AddRangeAsync(x);
		//await _productWriteRepository.SaveAsync();
		var totalCount = _productReadRepository.GetAll(false).Count();
		var products = _productReadRepository.GetAll(tracking: false).Select(p => new {
			p.Id,
			p.Name,
			p.Stock,
			p.Price,
			p.CreatedDate,
			p.UpdatedDate
		}).Skip(pagination.Page * pagination.Size).Take(pagination.Size);

		return Ok(new {
			totalCount,
			products
		});
	}

	[HttpGet("getById/{id:Guid}")]
	public async Task<IActionResult> GetById(Guid id) {
		return Ok(await _productReadRepository.GetByIdAsync(id, tracking: false));
	}

	[HttpPost]
	public async Task<IActionResult> Post(ViewModel_Create_Product model) {
		await _productWriteRepository.AddAsync(new() {
			Name = model.Name,
			Stock = model.Stock,
			Price = model.Price,
		});
		await _productWriteRepository.SaveAsync();
		return StatusCode(StatusCodes.Status201Created);
	}

	[HttpPut]
	public async Task<IActionResult> Put(ViewModel_Update_Product model) {
		var product = await _productReadRepository.GetByIdAsync(model.Id);
		product.Name = model.Name;
		product.Stock = model.Stock;
		product.Price = model.Price;
		await _productWriteRepository.SaveAsync();
		return Ok();
	}

	[HttpDelete("{id:Guid}")]
	public async Task<IActionResult> Delete(Guid id) {
		await _productWriteRepository.DeleteAsync(id);
		await _productWriteRepository.SaveAsync();
		return Ok();
	}
}