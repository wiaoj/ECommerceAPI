using MediatR;

namespace ECommerceAPI.Application.Features.Commands.Products.UpdateStockQrCodeToProduct;

public class UpdateStockQrCodeToProductCommandHandler : IRequestHandler<UpdateStockQrCodeToProductRequest, UpdateStockQrCodeToProductResponse> {
    public Task<UpdateStockQrCodeToProductResponse> Handle(UpdateStockQrCodeToProductRequest request, CancellationToken cancellationToken) {
        throw new NotImplementedException();
    }
}