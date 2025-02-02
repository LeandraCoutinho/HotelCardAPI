using System.Net;
using System.Net.Mail;
using HotelCard.Application.Contracts;
using HotelCard.Application.Dtos.Email;
using HotelCard.Application.Notification;
using HotelCard.Core.Settings;
using HotelCard.Domain.Entities;
using Microsoft.Extensions.Options;

namespace HotelCard.Application.Services;

public class EmailService : IEmailService
{
    private readonly EmailSettings _emailSettings;
    private readonly AppSettings _appSettings;
    private readonly INotificator _notificator;

    public EmailService(IOptions<EmailSettings> emailSettings, IOptions<AppSettings> appSettings, INotificator notificator)
    {
        _emailSettings = emailSettings.Value;
        _appSettings = appSettings.Value;
        _notificator = notificator;
    }


    public async Task SendEmailFirstAccess(Employee employee, string password)
    {
        var url = $"{_appSettings.UrlComum}/login";
        
        var body =
            $"Olá {employee.Name},<br><br>" +
            "Bem-vindo(a) ao nosso sistema.<br><br>" +
            $"Sua senha temporária é: {password}<br><br>" +
            $"<a href='{url}'>Login</a><br><br>" +
            "Se precisar de assistência, entre em contato com nosso suporte.<br><br>" +
            "Atenciosamente, Hotel Card";

        var mailData = new MailData
        {
            EmailSubject = "Senha temporária para acesso ao sistema.",
            EmailBody = body,
            EmailToId = employee.Email
        };
        
        await SendEmailAsync(mailData);
    }
    
    public async Task SendEmailAsync(MailData mailData)
    {
        var toEmail = mailData.EmailToId;
        var user = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(_emailSettings.User));
        var password = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(_emailSettings.Password));

        var smtpClient = new SmtpClient(_emailSettings.Server)
        {
            Port = _emailSettings.Port,
            Credentials = new NetworkCredential(user, password),
            EnableSsl = true,
        };

        var mailMessage = new MailMessage(user, toEmail)
        {
            Subject = mailData.EmailSubject,
            Body = mailData.EmailBody,
            IsBodyHtml = true
        };

        try
        {
            await Task.Run(() => smtpClient.Send(mailMessage));
        }
        catch (Exception)
        {
            _notificator.Handle("Ocorreu um erro ao enviar o e-mail");
        }
    }
}