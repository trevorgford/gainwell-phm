using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Gainwell.Models.Dapper;
using Microsoft.IdentityModel.Tokens;

namespace Gainwell.Services;

public class AuthenticationService(IConfiguration configuration) {

    private readonly string _secretKey = configuration["Jwt:SecretKey"] ?? throw new ArgumentNullException("Invalid JWT configuration");
    private readonly string _issuer = configuration["Jwt:Issuer"] ?? throw new ArgumentNullException("Invalid JWT configuration");
    private readonly string _audience = configuration["Jwt:Audience"] ?? throw new ArgumentNullException("Invalid JWT configuration");

    // public string GenerateJwtToken(UserModel user) {
    //     if(string.IsNullOrEmpty(user.Id?.ToString()) || string.IsNullOrEmpty(user.UserName)) throw new ArgumentException("Invalid user");

    //     var tokenHandler = new JwtSecurityTokenHandler();
    //     var key = Encoding.ASCII.GetBytes(_secretKey);
        
    //     var tokenDescriptor = new SecurityTokenDescriptor {
    //         Subject = new ClaimsIdentity([
    //             new Claim(ClaimTypes.NameIdentifier, user.Id?.ToString() ?? string.Empty),
    //             new Claim(ClaimTypes.Name, user.UserName),
    //             //new Claim(ClaimTypes.Role, user.Role)
    //         ]),
    //         Expires = DateTime.UtcNow.AddHours(1),
    //         Issuer = _issuer,
    //         Audience = _audience,
    //         SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
    //     };

    //     var token = tokenHandler.CreateToken(tokenDescriptor);
    //     return tokenHandler.WriteToken(token);
    // }

    public string GenerateJwtToken(int userId, int tenantId) {
        //if(string.IsNullOrEmpty(user.Id?.ToString()) || string.IsNullOrEmpty(user.UserName)) throw new ArgumentException("Invalid user");

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_secretKey);
        
        var tokenDescriptor = new SecurityTokenDescriptor {
            Subject = new ClaimsIdentity([
                new Claim("UserId", userId.ToString()),
                new Claim("TenantId", tenantId.ToString())
                //new Claim(ClaimTypes.Name, userName),
                //new Claim(ClaimTypes.Role, user.Role)
            ]),
            Expires = DateTime.UtcNow.AddHours(1),
            Issuer = _issuer,
            Audience = _audience,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

}
