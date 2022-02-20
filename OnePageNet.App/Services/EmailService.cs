using System.Net;
using System.Net.Mail;
using FluentEmail.Core;
using FluentEmail.Smtp;
using OnePageNet.App.Services.Interfaces;

namespace OnePageNet.App.Services;

public class EmailService : IEmailService
{
    public async Task SendEmail()
    {
        var sender = new SmtpSender(() => new SmtpClient("smtp.gmail.com")
        {
            UseDefaultCredentials = false,
            Port = 587,
            Credentials = new NetworkCredential("maga.ot.oz.spam@gmail.com", "EiKradecHaknaLiMe"),
            EnableSsl = true,
        });

        Email.DefaultSender = sender;
        var email = await Email
            .From("maga.ot.oz.spam@gmail.com", "kosyotester")
            .To("kosyo.marko@gmail.com", "kosyoistinski")
            .Subject("test email subject")
            .Body("This is the email body")
            .SendAsync();
    }
}