namespace ECommerceAPI.Application.ViewModels.Baskets;

public class VievModel_Update_BasketItem {
    public Guid BasketItemId { get; set; }
    public Guid ProductId { get; set; }
    public Int32 Quantity { get; set; }
}