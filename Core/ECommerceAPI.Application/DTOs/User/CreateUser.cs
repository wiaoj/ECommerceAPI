namespace ECommerceAPI.Application.DTOs.User;
public class CreateUser {
    public String NameSurname { get; set; }
    public String UserName { get; set; }
    public String Email { get; set; }
    public String Password { get; set; }
    public String PasswordConfirm { get; set; }
}