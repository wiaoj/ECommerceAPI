using MediatR;

namespace ECommerceAPI.Application.Features.Commands.Products.UpdateStockQrCodeToProduct;
public class UpdateStockQrCodeToProductRequest : IRequest<UpdateStockQrCodeToProductResponse> {
    public Guid Id { get; set; }
    public Int16 Stock { get; set; }
}