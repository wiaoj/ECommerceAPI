namespace ECommerceAPI.Application.Abstractions.Services;
public interface IMailService {
    Task SendMailAsync(String to, String subject, String body);
    Task SendMailAsync(String to, String subject, String body, Boolean isBodyHtml);
    Task SendMailAsync(String[] tos, String subject, String body);
    Task SendMailAsync(String[] tos, String subject, String body, Boolean isBodyHtml);

    Task SendPasswordResetMailAsync(String to, String userId, String resetToken);
}