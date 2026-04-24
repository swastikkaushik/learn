using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Api.Configurations;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

public class TokenService : ITokenService
{
    private readonly IOptions<JwtSettings> _jwtOptions;

    public TokenService(IOptions<JwtSettings> jwtOptions)
    {
        _jwtOptions = jwtOptions;
    }

    public AuthResponse Generate(User user)
    {
        var jwt = _jwtOptions.Value;
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expiresAt = DateTime.UtcNow.AddMinutes(jwt.ExpiryMinutes);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role)
        };

        var token = new JwtSecurityToken(
            issuer: jwt.Issuer,
            audience: jwt.Audience,
            claims: claims,
            expires: expiresAt,
            signingCredentials: credentials);

        return new AuthResponse
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            ExpiresAtUtc = expiresAt
        };
    }
}
