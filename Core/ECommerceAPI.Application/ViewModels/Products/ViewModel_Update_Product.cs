namespace ECommerceAPI.Application.ViewModels.Products;

public class ViewModel_Update_Product {
    public Guid Id { get; set; }
    public String Name { get; set; }
    public Int16 Stock { get; set; }
    public Decimal Price { get; set; }
}