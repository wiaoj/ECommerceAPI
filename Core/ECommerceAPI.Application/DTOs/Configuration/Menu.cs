namespace ECommerceAPI.Application.DTOs.Configuration;

public class Menu {
    public String Name { get; set; }
    public List<Action> Actions { get; set; } = new();
}