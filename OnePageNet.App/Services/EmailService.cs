using System.Net;
using System.Net.Mail;
using FluentEmail.Core;
using FluentEmail.Smtp;
using OnePageNet.App.Services.Interfaces;

namespace OnePageNet.App.Services;

public class EmailService : IEmailService
{
    private SmtpSender Sender { get; set; }
    
    // TODO Cleanup, remove hard coded values
    public EmailService()
    {
        Sender = new SmtpSender(() => new SmtpClient("smtp.gmail.com")
        {
            UseDefaultCredentials = false,
            Port = 587,
            Credentials = new NetworkCredential("maga.ot.oz.spam@gmail.com", "EiKradecHaknaLiMe"),
            EnableSsl = true,
        });
    }

    public async Task SendResetPasswordEmail(string to)
    {
        var callbackUrl = string.Empty;
        
        var body = $"Reset Password Please reset your password by clicking here: <a href=\"{callbackUrl}\">link</a>";

        await SendEmail(to, body);
    }
    
    public async Task SendEmail(string? to = null, string? body = null)
    {
        Email.DefaultSender = Sender;
        var email = await Email
            .From("maga.ot.oz.spam@gmail.com", "kosyotester")
            .To(to ?? "kosyo.marko@gmail.com", "kosyoistinski")
            .Subject("test email subject")
            .Body(body ?? "This is the email body")
            .SendAsync();
    }
}