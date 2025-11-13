using System.ComponentModel.DataAnnotations;

namespace DevsPros.Diabelife.Platform.API.Authentication.Interfaces.REST.DTOs;

public class LoginRequestDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;
}