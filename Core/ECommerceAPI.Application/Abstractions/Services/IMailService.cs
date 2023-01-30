namespace ECommerceAPI.Application.Abstractions.Services;
public interface IMailService {
    Task SendMessageAsync(String to, String subject, String body);
    Task SendMessageAsync(String to, String subject, String body, Boolean isBodyHtml);
    Task SendMessageAsync(String[] tos, String subject, String body);
    Task SendMessageAsync(String[] tos, String subject, String body, Boolean isBodyHtml);
}