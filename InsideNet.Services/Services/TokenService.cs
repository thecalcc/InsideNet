using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using InsideNet.Services.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace InsideNet.Services.Services;

public class TokenService : ITokenService
{
    private readonly IUserService _userService;
    private const double ExpiryDurationMinutes = 30;

    public TokenService(IUserService userService)
    {
        _userService = userService;
    }
    
    public async Task<string> BuildToken(string key, string issuer, string userId)
    {
        var user = await _userService.GetUserEntityById(userId);
        
        var claims = new[]
        {
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.NameIdentifier, userId)
        };

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
        
        var tokenDescriptor = new JwtSecurityToken(issuer, issuer, claims,
            expires: DateTime.Now.AddMinutes(ExpiryDurationMinutes), signingCredentials: credentials);
        
        return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
    }

    public bool IsTokenValid(string key, string issuer, string token)
    {
        var mySecret = Encoding.UTF8.GetBytes(key);
        var mySecurityKey = new SymmetricSecurityKey(mySecret);
        var tokenHandler = new JwtSecurityTokenHandler();
        try
        {
            tokenHandler.ValidateToken(token,
                new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = issuer,
                    ValidAudience = issuer,
                    IssuerSigningKey = mySecurityKey,
                }, out SecurityToken validatedToken);
        }
        catch
        {
            return false;
        }

        return true;
    }
}