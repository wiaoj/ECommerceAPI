namespace ECommerceAPI.Application.DTOs.Order;
public class CreateOrder {
    public Guid BasketId { get; set; }
    public String Address { get; set; }
    public String Description { get; set; }
}