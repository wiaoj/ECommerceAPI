using MediatR;

namespace ECommerceAPI.Application.Features.Queries.Baskets.GetBasketItems;

public class GetBasketItemsQueryRequest : IRequest<List<GetBasketItemsQueryResponse>> { }