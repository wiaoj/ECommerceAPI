namespace ECommerceAPI.Application.Abstractions.Services;
public interface IProductService {
    Task<Byte[]> QrCodeToProductAsync(Guid productId);
}
