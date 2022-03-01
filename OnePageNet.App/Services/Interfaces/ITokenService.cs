using OnePageNet.App.Data.Entities;

namespace OnePageNet.App.Services.Interfaces;

public interface ITokenService
{
    string BuildToken(string key, string issuer, ApplicationUser user);
    bool IsTokenValid(string key, string issuer, string token);
}