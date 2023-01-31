namespace ECommerceAPI.Application.DTOs.Order;

public class CompletedOrderDto {
    public String Email { get; set; }
    public String OrderCode { get; set; }
    public String UserNameSurname { get; set; }
    public DateTime OrderDate { get; set; }
}