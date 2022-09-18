namespace ECommerceAPI.Application.RequestParamaters;
public record Pagination {
    public Int32 Page { get; set; } = 0;
    public Int32 Size { get; set; } = 5;
}