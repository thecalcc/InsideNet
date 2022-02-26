namespace OnePageNet.App.Services.Interfaces;

public interface IEmailService
{
    Task SendEmail(string? to = null, string? body = null);
    Task SendResetPasswordEmail(string to);
}    