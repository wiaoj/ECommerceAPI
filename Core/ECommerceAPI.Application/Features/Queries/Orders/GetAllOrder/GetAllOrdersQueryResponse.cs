namespace ECommerceAPI.Application.Features.Queries.Orders.GetAllOrder;

public class GetAllOrdersQueryResponse {
    public Int32 TotalOrderCount { get; set; }
    public Object Orders { get; set; }
}
