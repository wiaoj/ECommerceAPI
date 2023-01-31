namespace ECommerceAPI.Application.Features.Queries.Orders.GetOrderById;
public class GetByIdOrderQueryResponse {
    public Guid Id { get; set; }
    public String OrderCode { get; set; }
    public String Address { get; set; }
    public String Description { get; set; }
    public Object BasketItems { get; set; }
    public DateTime CreatedDate { get; set; }
    public Boolean Completed { get; set; }
}