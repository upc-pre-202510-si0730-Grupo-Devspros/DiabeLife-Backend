using DevsPros.Diabelife.Platform.API.Authentication.Application.Internal.CommandServices;
using DevsPros.Diabelife.Platform.API.Authentication.Interfaces.REST.DTOs;
using DevsPros.Diabelife.Platform.API.Shared.Domain.Model;
using DevsPros.Diabelife.Platform.API.Shared.Domain.Repositories;

namespace DevsPros.Diabelife.Platform.API.Authentication.Application.Internal.CommandServices;

public class AuthCommandService : IAuthCommandService
{
    private readonly IUserRepository _userRepository;

    public AuthCommandService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task RegisterAsync(RegisterRequestDto request)
    {
        // Check if user with email already exists
        var existingUser = await _userRepository.FindByEmailAsync(request.Email);
        if (existingUser != null)
        {
            throw new InvalidOperationException("User with this email already exists");
        }

        // Hash password (using BCrypt for security)
        var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

        // Create new user
        var user = new User
        {
            Email = request.Email,
            PasswordHash = passwordHash
        };

        await _userRepository.AddAsync(user);
        await _userRepository.SaveChangesAsync();
    }
}