using System.ComponentModel.DataAnnotations;

namespace DevsPros.Diabelife.Platform.API.Authentication.Interfaces.REST.DTOs;

public class RegisterRequestDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    [MinLength(6)]
    public string Password { get; set; } = string.Empty;
}