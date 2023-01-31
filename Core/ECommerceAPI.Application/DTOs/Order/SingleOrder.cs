namespace ECommerceAPI.Application.DTOs.Order;

public class SingleOrder {
    public Guid Id { get; set; }
    public String OrderCode { get; set; }
    public String Address { get; set; }
    public String Description { get; set; }
    public Object BasketItems { get; set; }
    public DateTime CreatedDate { get; set; }
    public Boolean Completed { get; set; }
}