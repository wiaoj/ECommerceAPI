using ECommerceAPI.Application.Abstractions.Hubs;
using ECommerceAPI.Application.Abstractions.Services;
using MediatR;

namespace ECommerceAPI.Application.Features.Commands.Orders.CreateOrderCommand;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommandRequest, CreateOrderCommandResponse> {

    private readonly IOrderService _orderService;
    private readonly IBasketService _basketService;
    private readonly IOrderHubService _orderHubService;

    public CreateOrderCommandHandler(IOrderService orderService,
                                     IBasketService basketService,
                                     IOrderHubService orderHubService) {
        _orderService = orderService;
        _basketService = basketService;
        _orderHubService = orderHubService;
    }

    public async Task<CreateOrderCommandResponse> Handle(CreateOrderCommandRequest request, CancellationToken cancellationToken) {
        await _orderService.CreateOrderAsync(new() {
            BasketId = _basketService.GetUserActiveBasketAsync.Id,
            Address = request.Address,
            Description = request.Description,
        });

        await _orderHubService.OrderAddedMessageAsync("Heyy, yeni bir sipariş gelmiştir!");

        return new();
    }
}