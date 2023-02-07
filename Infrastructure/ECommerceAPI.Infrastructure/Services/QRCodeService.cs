using ECommerceAPI.Application.Abstractions.Services;
using QRCoder;

namespace ECommerceAPI.Infrastructure.Services;
public class QRCodeService : IQRCodeService {
    public Byte[] GenerateQRCode(String value) {
        QRCodeGenerator generator = new();
        QRCodeData qRCodeData = generator.CreateQrCode(value, QRCodeGenerator.ECCLevel.Q);
        PngByteQRCode qRCode = new(qRCodeData);
        Byte[] byteGraphic = qRCode.GetGraphic(10, new Byte[] { 84, 99, 71 }, new Byte[] { 240, 240, 240 });
        return byteGraphic;
    }
}