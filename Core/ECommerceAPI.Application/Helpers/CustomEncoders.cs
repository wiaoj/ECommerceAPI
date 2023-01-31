using Microsoft.AspNetCore.WebUtilities;
using System.Text;

namespace ECommerceAPI.Application.Helpers;
public static class CustomEncoders {
    public static String UrlEncode(this String value) {
        Byte[] bytes = Encoding.UTF8.GetBytes(value);
        return WebEncoders.Base64UrlEncode(bytes);
    }

    public static String UrlDecode(this String value) {
        Byte[] bytes = WebEncoders.Base64UrlDecode(value);
        return Encoding.UTF8.GetString(bytes);
    }
}