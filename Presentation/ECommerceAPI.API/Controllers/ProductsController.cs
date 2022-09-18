using ECommerceAPI.Application.Repositories.Products;
using ECommerceAPI.Application.ViewModels.Products;
using ECommerceAPI.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using ECommerceAPI.Application.RequestParamaters;
using ECommerceAPI.Application.Abstractions.Storage;
using ECommerceAPI.Application.Repositories.FileRepositories.ProductImageFiles;
using Microsoft.EntityFrameworkCore;
using ECommerceAPI.Domain.Entities.Files;
using MediatR;
using ECommerceAPI.Application.Features.Commands.Products.CreateProduct;
using ECommerceAPI.Application.Features.Queries.Products.GetAllProduct;
using ECommerceAPI.Application.Features.Queries.Products.GetByIdProduct;
using ECommerceAPI.Application.Features.Commands.Products.UpdateProduct;
using ECommerceAPI.Application.Features.Commands.Products.DeleteProduct;
using ECommerceAPI.Application.Features.Commands.ProductImageFiles.UploadProductImage;
using ECommerceAPI.Application.Features.Commands.ProductImageFiles.DeleteProductImage;
using ECommerceAPI.Application.Features.Queries.ProductImageFiles.GetProductImages;

namespace ECommerceAPI.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase {
    private readonly IProductWriteRepository _productWriteRepository;
    private readonly IProductReadRepository _productReadRepository;
    private readonly IStorageService _storageService;
    private readonly IProductImageFileWriteRepository _productImageFileWriteRepository;
    private readonly IConfiguration _configuration;




    private readonly IMediator _mediator;

    public ProductsController(IProductWriteRepository productWriteRepository, IProductReadRepository productReadRepository, IStorageService storageService, IProductImageFileWriteRepository productImageFileWriteRepository, IConfiguration configuration, IMediator mediator) {
        this._productWriteRepository = productWriteRepository;
        this._productReadRepository = productReadRepository;
        this._storageService = storageService;
        this._productImageFileWriteRepository = productImageFileWriteRepository;
        this._configuration = configuration;
        this._mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] GetAllProductQueryRequest getAllProductQueryRequest) {
        GetAllProductQueryResponse response = await _mediator.Send(getAllProductQueryRequest);
        return Ok(response);
    }

    [HttpGet("getById/{Id:Guid}")]
    public async Task<IActionResult> GetById([FromRoute] GetByIdProductQeuryRequest byIdProductQeuryRequest) {
        GetByIdProductQeuryResponse response = await _mediator.Send(byIdProductQeuryRequest);
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Post(CreateProductCommandRequest createProductCommandRequest) {
        CreateProductCommandResponse response = await _mediator.Send(createProductCommandRequest);
        return StatusCode(StatusCodes.Status201Created/*, response*/);
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] UpdateProductCommandRequest updateProductCommandRequest) {
        UpdateProductCommandResponse response = await _mediator.Send(updateProductCommandRequest);
        return Ok(response);
    }

    [HttpDelete("{Id:Guid}")]
    public async Task<IActionResult> Delete([FromRoute] DeleteProductCommandRequest deleteProductCommandRequest) {
        DeleteProductCommandResponse response = await _mediator.Send(deleteProductCommandRequest);
        return Ok(response);
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Upload([FromQuery] UploadProductImageCommandRequest uploadProductImageCommandRequest) {
        uploadProductImageCommandRequest.FormFiles = Request.Form.Files;
        UploadProductImageCommandResponse response = await _mediator.Send(uploadProductImageCommandRequest);
        return Ok(response);
    }

    [HttpDelete("[action]/{Id}")]
    public async Task<IActionResult> DeleteProductImage([FromRoute] DeleteProductImageCommandRequest deleteProductImageCommandRequest, [FromQuery] Guid imageId) {
        deleteProductImageCommandRequest.ImageId = imageId;
        DeleteProductImageCommandResponse response = await _mediator.Send(deleteProductImageCommandRequest);
        return Ok(response);
    }

    [HttpGet("[action]/{Id}")]
    public async Task<IActionResult> GetProductImages([FromRoute] GetProductImagesQueryRequest getProductImagesQueryRequest) {
        List<GetProductImagesQueryResponse>? response = await _mediator.Send(getProductImagesQueryRequest);
        return Ok(response);
    }
}