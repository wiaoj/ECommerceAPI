using ECommerceAPI.Application.Abstractions.Services;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;

namespace ECommerceAPI.Infrastructure.Services;
public class MailService : IMailService {
    private readonly IConfiguration _configuration;

    public MailService(IConfiguration configuration) {
        _configuration = configuration;
    }

    public async Task SendMessageAsync(String to, String subject, String body) {
        await SendMessageAsync(to, subject, body, true);
    }

    public async Task SendMessageAsync(String to, String subject, String body, Boolean isBodyHtml) {
        await SendMessageAsync(new[] { to }, subject, body, isBodyHtml);

    }

    public async Task SendMessageAsync(String[] tos, String subject, String body) {
        await SendMessageAsync(tos, subject, body, true);
    }

    public async Task SendMessageAsync(String[] tos, String subject, String body, Boolean isBodyHtml) {
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
}