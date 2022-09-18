using MediatR;

namespace ECommerceAPI.Application.Features.Commands.Products.CreateProduct;
public class CreateProductCommandRequest : IRequest<CreateProductCommandResponse> {
    public String Name { get; set; }
    public Int16 Stock { get; set; }
    public Decimal Price { get; set; }
}