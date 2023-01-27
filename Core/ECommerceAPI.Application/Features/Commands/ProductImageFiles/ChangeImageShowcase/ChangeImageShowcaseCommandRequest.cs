using MediatR;

namespace ECommerceAPI.Application.Features.Commands.ProductImageFiles.ChangeImageShowcase;
public class ChangeImageShowcaseCommandRequest : IRequest<ChangeImageShowcaseCommandResponse> {
    public Guid ImageId { get; set; }
    public Guid ProductId { get; set; }
}