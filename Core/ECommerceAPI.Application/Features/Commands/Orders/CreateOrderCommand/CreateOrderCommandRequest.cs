using MediatR;

namespace ECommerceAPI.Application.Features.Commands.Orders.CreateOrderCommand;

public class CreateOrderCommandRequest : IRequest<CreateOrderCommandResponse> {
    public Guid BasketId { get; set; }
    public String Address { get; set; }
    public String Description { get; set; }
}
