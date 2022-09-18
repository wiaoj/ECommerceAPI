using MediatR;

namespace ECommerceAPI.Application.Features.Commands.Products.UpdateProduct;
public class UpdateProductCommandRequest : IRequest<UpdateProductCommandResponse> {
    public Guid Id { get; set; }
    public String Name { get; set; }
    public Int16 Stock { get; set; }
    public Decimal Price { get; set; }
}