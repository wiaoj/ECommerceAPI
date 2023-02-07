namespace ECommerceAPI.Application.Abstractions.Services;
public interface IQRCodeService {
    public Byte[] GenerateQRCode(String value);
}