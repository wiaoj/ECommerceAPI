using ECommerceAPI.Application.Abstractions.Services;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace ECommerceAPI.Infrastructure.Services;
public class MailService : IMailService {
    private readonly IConfiguration _configuration;

    public MailService(IConfiguration configuration) {
        _configuration = configuration;
    }

    public async Task SendMailAsync(String to, String subject, String body) {
        await SendMailAsync(to, subject, body, true);
    }

    public async Task SendMailAsync(String to, String subject, String body, Boolean isBodyHtml) {
        await SendMailAsync(new[] { to }, subject, body, isBodyHtml);

    }

    public async Task SendMailAsync(String[] tos, String subject, String body) {
        await SendMailAsync(tos, subject, body, true);
    }

    public async Task SendMailAsync(String[] tos, String subject, String body, Boolean isBodyHtml) {
        MailMessage mailMessage = new();
        mailMessage.IsBodyHtml = isBodyHtml;

        foreach(var to in tos) {
            mailMessage.To.Add(to);
        }

        mailMessage.Subject = subject;
        mailMessage.Body = body;
        mailMessage.From = new(_configuration["MailService:Username"], "E-Commerce", System.Text.Encoding.UTF8);

        SmtpClient smtpClient = new() {
            Credentials = new NetworkCredential(_configuration["MailService:Username"], _configuration["MailService:Password"]),
            Port = Int32.Parse(_configuration["MailService:Port"]),
            EnableSsl = true,
            Host = _configuration["MailService:Host"]
        };

        await smtpClient.SendMailAsync(mailMessage);
    }

    public async Task SendPasswordResetMailAsync(String to, String userId, String resetToken) {

        StringBuilder mail = new();
        mail.AppendLine("Merhaba<br>Eğer yeni şifre talebinde bulunduysanız aşağıdaki linkten şifrenizi yenileyebilirsiniz.<br><strong><a target=\"_blank\" href=\"");
        mail.AppendLine(_configuration["AngularClientUrl"]);
        mail.AppendLine("/password-update/");
        mail.AppendLine(userId);
        mail.AppendLine("/");
        mail.AppendLine(resetToken);
        mail.AppendLine("\">");
        mail.AppendLine("Yeni şifre talebi için tıklayınız</a></strong><br><br><span style=\"font-size:12px;\">Eğer talebiniz dışında gerçekleştirildiyse ciddiye almayınız.</span><br><br><br><br><br>E-Commerce");

        await SendMailAsync(to, "Şifre Yenileme Talebi", mail.ToString());
    }

    public async Task SendCompletedOrderMailAsync(String to, String userNameSurname, String orderCode, DateTime orderDate) {
        String mail = $"Sayın {userNameSurname} merhaba,<br>" +
            $"{orderDate} tarihinde vermiş olduğunuz {orderCode} kodlu siparişiniz tamamlanmış ve kargo firmasına iletilmiştir.<br> İyi günler dileriz.";
        await SendMailAsync(to, $"{orderCode} numaralı siparişiniz tamamlandı", mail);
    }
}