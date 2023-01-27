namespace ECommerceAPI.Domain.Entities.Files;
public class ProductImageFile : File {
    public Boolean Showcase { get; set; }
    public ICollection<Product> Products { get; set; }
}