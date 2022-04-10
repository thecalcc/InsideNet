namespace InsideNet.Services.Services.Interfaces;

public interface ITokenService
{
    Task<string> BuildToken(string key, string issuer, string userId);
    bool IsTokenValid(string key, string issuer, string token);
}