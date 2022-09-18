using MediatR;

namespace ECommerceAPI.Application.Features.Commands.Products.DeleteProduct;
public class DeleteProductCommandRequest : IRequest<DeleteProductCommandResponse> {
    public Guid Id { get; set; }
}