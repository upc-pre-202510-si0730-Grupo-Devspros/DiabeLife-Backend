using System.ComponentModel.DataAnnotations;

namespace DevsPros.Diabelife.Platform.API.Authentication.Interfaces.REST.DTOs;

public class LoginRequestDto
{
    [Required]
    public string Username { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;
}