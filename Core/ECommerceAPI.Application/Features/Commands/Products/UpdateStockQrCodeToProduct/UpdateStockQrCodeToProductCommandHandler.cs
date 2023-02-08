using ECommerceAPI.Application.Abstractions.Services;
using MediatR;

namespace ECommerceAPI.Application.Features.Commands.Products.UpdateStockQrCodeToProduct;

public class UpdateStockQrCodeToProductCommandHandler : IRequestHandler<UpdateStockQrCodeToProductCommandRequest, UpdateStockQrCodeToProductCommandResponse> {

    private readonly IProductService _productService;

    public UpdateStockQrCodeToProductCommandHandler(IProductService productService) {
        _productService = productService;
    }

    public async Task<UpdateStockQrCodeToProductCommandResponse> Handle(UpdateStockQrCodeToProductCommandRequest request, CancellationToken cancellationToken) {
        await _productService.StockUpdateToProductAsync(request.Id, request.Stock);
        return new();
    }
}