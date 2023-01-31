using ECommerceAPI.Application.Abstractions.Services;
using MediatR;

namespace ECommerceAPI.Application.Features.Commands.Orders.ComplateOrderCommand;

public class ComplateOrderCommandHandler : IRequestHandler<ComplateOrderCommandRequest, ComplateOrderCommandResponse> {
    private readonly IOrderService _orderService;

    public ComplateOrderCommandHandler(IOrderService orderService) {
        _orderService = orderService;
    }

    public async Task<ComplateOrderCommandResponse> Handle(ComplateOrderCommandRequest request, CancellationToken cancellationToken) {
        await _orderService.CompleteOrderAsync(request.Id);
        return new();
    }
}