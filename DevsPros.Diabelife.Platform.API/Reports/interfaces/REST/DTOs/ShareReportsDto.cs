using System.ComponentModel.DataAnnotations;

namespace DevsPros.Diabelife.Platform.API.Reports.Interfaces.REST.DTOs;

public class ShareReportsDto
{
    [Required]
    public List<int> ReportIds { get; set; } = new List<int>();

    [Required]
    public bool Shared { get; set; }
}