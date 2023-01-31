using ECommerceAPI.Application.Enums;

namespace ECommerceAPI.Application.CustomAttributes;
public class AuthorizeDefinitionAttribute : Attribute {
    public String Menu { get; set; }
    public String Definition { get; set; }
    public ActionType ActionType { get; set; }
}