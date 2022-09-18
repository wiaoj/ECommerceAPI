using MediatR;

namespace ECommerceAPI.Application.Features.Commands.ProductImageFiles.DeleteProductImage;

public class DeleteProductImageCommandRequest : IRequest<DeleteProductImageCommandResponse> {
    public Guid Id { get; set; }
    public Guid? ImageId { get; set; }
}