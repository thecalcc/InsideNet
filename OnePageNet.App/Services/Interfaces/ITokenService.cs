using OnePageNet.App.Data.Entities;
using OnePageNet.App.Data.Models;

namespace OnePageNet.App.Services.Interfaces;

public interface ITokenService
{
    Task<string> BuildToken(string key, string issuer, string userId);
    bool IsTokenValid(string key, string issuer, string token);
}