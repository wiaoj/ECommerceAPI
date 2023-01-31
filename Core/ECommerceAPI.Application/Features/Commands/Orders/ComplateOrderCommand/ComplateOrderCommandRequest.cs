using MediatR;

namespace ECommerceAPI.Application.Features.Commands.Orders.ComplateOrderCommand;
public class ComplateOrderCommandRequest : IRequest<ComplateOrderCommandResponse> {
    public Guid Id { get; set; }
}