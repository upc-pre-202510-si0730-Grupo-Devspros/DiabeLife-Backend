using System.ComponentModel.DataAnnotations;

namespace DevsPros.Diabelife.Platform.API.Reports.Interfaces.REST.DTOs;

public class CreateReportDto
{
    [Required]
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;

    [Required]
    public DateTime Date { get; set; }

    [Required]
    [MaxLength(100)]
    public string Type { get; set; } = string.Empty;

    [Required]
    public string Data { get; set; } = string.Empty;

    public bool Selected { get; set; } = false;

    public bool Shared { get; set; } = false;
}