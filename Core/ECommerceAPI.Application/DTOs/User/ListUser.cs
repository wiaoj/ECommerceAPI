namespace ECommerceAPI.Application.DTOs.User;
public class ListUser {
    public String Id { get; set; }
    public String Email { get; set; }
    public String UserName { get; set; }
    public String NameSurname { get; set; }
    public Boolean TwoFactorEnabled { get; set; }
}