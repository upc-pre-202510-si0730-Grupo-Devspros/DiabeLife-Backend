namespace DevsPros.Diabelife.Platform.API.Authentication.Interfaces.REST.DTOs;

public class LoginResponseDto
{
    public string Token { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}