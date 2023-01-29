namespace ECommerceAPI.Application.Features.Queries.Baskets.GetBasketItems;

public class GetBasketItemsQueryResponse {
    public Guid BasketItemId { get; set; }
    public String Name { get; set; }
    public Decimal Price { get; set; }
    public Int32 Quantity { get; set; }
}