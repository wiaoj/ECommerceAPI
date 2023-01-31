using ECommerceAPI.Application.Abstractions.Services;
using ECommerceAPI.Application.DTOs.Order;
using MediatR;

namespace ECommerceAPI.Application.Features.Commands.Orders.ComplateOrderCommand;

public class ComplateOrderCommandHandler : IRequestHandler<ComplateOrderCommandRequest, ComplateOrderCommandResponse> {
    private readonly IOrderService _orderService;
    private readonly IMailService _mailService;

    public ComplateOrderCommandHandler(IOrderService orderService, IMailService mailService) {
        _orderService = orderService;
        _mailService = mailService;
    }

    public async Task<ComplateOrderCommandResponse> Handle(ComplateOrderCommandRequest request, CancellationToken cancellationToken) {
       (Boolean status, CompletedOrderDto order) = await _orderService.CompleteOrderAsync(request.Id);

        if (status) {
            await _mailService.SendCompletedOrderMailAsync(order.Email,order.UserNameSurname, order.OrderCode, order.OrderDate);
        }

        return new();
    }
}