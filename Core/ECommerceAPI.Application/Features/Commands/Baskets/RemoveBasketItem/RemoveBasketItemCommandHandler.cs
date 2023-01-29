using ECommerceAPI.Application.Abstractions.Services;
using MediatR;

namespace ECommerceAPI.Application.Features.Commands.Baskets.RemoveBasketItem;
public class RemoveBasketItemCommandHandler : IRequestHandler<RemoveBasketItemCommandRequest, RemoveBasketItemCommandResponse> {
    private readonly IBasketService _basketService;

    public RemoveBasketItemCommandHandler(IBasketService basketService) {
        _basketService = basketService;
    }
    public async Task<RemoveBasketItemCommandResponse> Handle(RemoveBasketItemCommandRequest request, CancellationToken cancellationToken) {
        await _basketService.RemoveBasketItemAsync(request.BasketItemId);

        return new();
    }
}