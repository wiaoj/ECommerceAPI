using ECommerceAPI.Application.Enums;

namespace ECommerceAPI.Application.DTOs.Configuration;
public class Action {
    public String Code { get; set; }
    public String ActionType { get; set; }
    public String HttpType { get; set; }
    public String Definition { get; set; }
}