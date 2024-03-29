﻿using Azure;
using ECommerceAPI.Application.Abstractions.Services;
using ECommerceAPI.Application.Consts;
using ECommerceAPI.Application.CustomAttributes;
using ECommerceAPI.Application.Enums;
using ECommerceAPI.Application.Features.Commands.ProductImageFiles.ChangeImageShowcase;
using ECommerceAPI.Application.Features.Commands.ProductImageFiles.DeleteProductImage;
using ECommerceAPI.Application.Features.Commands.ProductImageFiles.UploadProductImage;
using ECommerceAPI.Application.Features.Commands.Products.CreateProduct;
using ECommerceAPI.Application.Features.Commands.Products.DeleteProduct;
using ECommerceAPI.Application.Features.Commands.Products.UpdateProduct;
using ECommerceAPI.Application.Features.Commands.Products.UpdateStockQrCodeToProduct;
using ECommerceAPI.Application.Features.Queries.ProductImageFiles.GetProductImages;
using ECommerceAPI.Application.Features.Queries.Products.GetAllProduct;
using ECommerceAPI.Application.Features.Queries.Products.GetByIdProduct;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase {

    private readonly IMediator _mediator;
    private readonly IProductService _productService;

    public ProductsController(IMediator mediator, IProductService productService) {
        _mediator = mediator;
        _productService = productService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] GetAllProductQueryRequest getAllProductQueryRequest) {
        GetAllProductQueryResponse response = await _mediator.Send(getAllProductQueryRequest);
        return Ok(response);
    }

    [HttpGet("qrcode/{productId}")]
    public async Task<IActionResult> GetQrCodeToProduct([FromRoute] Guid productId) {
        var data = await _productService.QrCodeToProductAsync(productId);
        return File(data, "image/png");
    }

    [HttpPut("qrcode")]
    public async Task<IActionResult> UpdateStockQrCodeToProduct([FromBody] UpdateStockQrCodeToProductCommandRequest request) {
        UpdateStockQrCodeToProductCommandResponse response = await _mediator.Send(request);
        return Ok(response);
    }

    [HttpGet("getById/{Id:Guid}")]
    public async Task<IActionResult> GetById([FromRoute] GetByIdProductQeuryRequest byIdProductQeuryRequest) {
        GetByIdProductQeuryResponse response = await _mediator.Send(byIdProductQeuryRequest);
        return Ok(response);
    }


    [HttpPost]
    [Authorize(AuthenticationSchemes = "Admin")]
    [AuthorizeDefinition(
        Menu = AuthorizeDefinitionConstants.Products,
        ActionType = ActionType.Writing,
        Definition = "Create Product"
        )]
    public async Task<IActionResult> Post(CreateProductCommandRequest createProductCommandRequest) {
        CreateProductCommandResponse response = await _mediator.Send(createProductCommandRequest);
        return StatusCode(StatusCodes.Status201Created/*, response*/);
    }

    [HttpPut]
    [Authorize(AuthenticationSchemes = "Admin")]
    [AuthorizeDefinition(
        Menu = AuthorizeDefinitionConstants.Products,
        ActionType = ActionType.Updating,
        Definition = "Update Product"
        )]
    public async Task<IActionResult> Put([FromBody] UpdateProductCommandRequest updateProductCommandRequest) {
        UpdateProductCommandResponse response = await _mediator.Send(updateProductCommandRequest);
        return Ok(response);
    }

    [HttpDelete("{Id:Guid}")]
    [Authorize(AuthenticationSchemes = "Admin")]
    [AuthorizeDefinition(
        Menu = AuthorizeDefinitionConstants.Products,
        ActionType = ActionType.Deleting,
        Definition = "Delete Product"
        )]
    public async Task<IActionResult> Delete([FromRoute] DeleteProductCommandRequest deleteProductCommandRequest) {
        DeleteProductCommandResponse response = await _mediator.Send(deleteProductCommandRequest);
        return Ok(response);
    }

    [HttpPost("[action]")]
    [Authorize(AuthenticationSchemes = "Admin")]
    [AuthorizeDefinition(
        Menu = AuthorizeDefinitionConstants.Products,
        ActionType = ActionType.Writing,
        Definition = "Upload Product Image"
        )]
    public async Task<IActionResult> Upload([FromQuery] UploadProductImageCommandRequest uploadProductImageCommandRequest) {
        uploadProductImageCommandRequest.FormFiles = Request.Form.Files;
        UploadProductImageCommandResponse response = await _mediator.Send(uploadProductImageCommandRequest);
        return Ok(response);
    }

    [HttpDelete("[action]/{Id:Guid}")]
    [Authorize(AuthenticationSchemes = "Admin")]
    [AuthorizeDefinition(
        Menu = AuthorizeDefinitionConstants.Products,
        ActionType = ActionType.Deleting,
        Definition = "Delete Product Image"
        )]
    public async Task<IActionResult> DeleteProductImage([FromRoute] DeleteProductImageCommandRequest deleteProductImageCommandRequest, [FromQuery] Guid imageId) {
        deleteProductImageCommandRequest.ImageId = imageId;
        DeleteProductImageCommandResponse response = await _mediator.Send(deleteProductImageCommandRequest);
        return Ok(response);
    }

    [HttpGet("[action]/{Id:Guid}")]
    [Authorize(AuthenticationSchemes = "Admin")]
    [AuthorizeDefinition(
        Menu = AuthorizeDefinitionConstants.Products,
        ActionType = ActionType.Reading,
        Definition = "Get Product Images"
        )]
    public async Task<IActionResult> GetProductImages([FromRoute] GetProductImagesQueryRequest getProductImagesQueryRequest) {
        List<GetProductImagesQueryResponse>? response = await _mediator.Send(getProductImagesQueryRequest);
        return Ok(response);
    }

    //[HttpGet("[action]/{imageId:Guid}/{productId:Guid}")]
    [HttpGet("[action]")]
    [Authorize(AuthenticationSchemes = "Admin")]
    [AuthorizeDefinition(
        Menu = AuthorizeDefinitionConstants.Products,
        ActionType = ActionType.Updating,
        Definition = "Change Product Showcase Image"
        )]
    public async Task<IActionResult> ChangeImageShowcase([FromQuery] ChangeImageShowcaseCommandRequest changeImageShowcaseCommandRequest) {
        ChangeImageShowcaseCommandResponse response = await _mediator.Send(changeImageShowcaseCommandRequest);

        return Ok(response);
    }
}