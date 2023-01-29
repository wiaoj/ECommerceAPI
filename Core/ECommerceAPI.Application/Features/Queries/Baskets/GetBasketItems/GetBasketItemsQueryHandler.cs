using ECommerceAPI.Application.Abstractions.Services;
using MediatR;

namespace ECommerceAPI.Application.Features.Queries.Baskets.GetBasketItems;
public class GetBasketItemsQueryHandler : IRequestHandler<GetBasketItemsQueryRequest, List<GetBasketItemsQueryResponse>> {
    private readonly IBasketService _basketService;

    public GetBasketItemsQueryHandler(IBasketService basketService) {
        _basketService = basketService;
    }

    public async Task<List<GetBasketItemsQueryResponse>> Handle(GetBasketItemsQueryRequest request, CancellationToken cancellationToken) {
        var basketItems = await _basketService.GetBasketItemsAsync();

        return basketItems.Select(basketItem => new GetBasketItemsQueryResponse {
            BasketItemId = basketItem.Id,
            Name = basketItem.Product.Name,
            Price = basketItem.Product.Price,
            Quantity = basketItem.Quantity,
        }).ToList();
    }
}