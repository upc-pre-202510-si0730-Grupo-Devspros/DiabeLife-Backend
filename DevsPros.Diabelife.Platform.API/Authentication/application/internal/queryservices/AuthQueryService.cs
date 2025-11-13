using DevsPros.Diabelife.Platform.API.Authentication.Application.Internal.QueryServices;
using DevsPros.Diabelife.Platform.API.Authentication.Interfaces.REST.DTOs;
using DevsPros.Diabelife.Platform.API.Shared.Domain.Repositories;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DevsPros.Diabelife.Platform.API.Authentication.Application.Internal.QueryServices;

public class AuthQueryService : IAuthQueryService
{
    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _configuration;

    public AuthQueryService(IUserRepository userRepository, IConfiguration configuration)
    {
        _userRepository = userRepository;
        _configuration = configuration;
    }

    public async Task<string> LoginAsync(LoginRequestDto request)
    {
        // Find user by email
        var user = await _userRepository.FindByEmailAsync(request.Email);
        if (user == null)
        {
            throw new UnauthorizedAccessException("Invalid email or password");
        }

        // Verify password
        if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
        {
            throw new UnauthorizedAccessException("Invalid email or password");
        }

        // Generate JWT token
        return GenerateJwtToken(user);
    }

    private string GenerateJwtToken(DevsPros.Diabelife.Platform.API.Shared.Domain.Model.User user)
    {
        var jwtKey = _configuration["Jwt:Key"] ?? "your-secret-key-here-make-it-longer-than-32-characters";
        var jwtIssuer = _configuration["Jwt:Issuer"] ?? "DiabeLifeAPI";
        var jwtAudience = _configuration["Jwt:Audience"] ?? "DiabeLifeClient";

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var token = new JwtSecurityToken(
            issuer: jwtIssuer,
            audience: jwtAudience,
            claims: claims,
            expires: DateTime.UtcNow.AddDays(7),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}