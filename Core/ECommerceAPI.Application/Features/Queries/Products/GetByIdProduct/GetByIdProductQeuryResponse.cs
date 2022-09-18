namespace ECommerceAPI.Application.Features.Queries.Products.GetByIdProduct;

public class GetByIdProductQeuryResponse {
    public Guid Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public virtual DateTime UpdatedDate { get; set; }
    public String Name { get; set; }
    public Int16 Stock { get; set; }
    public Decimal Price { get; set; }
}