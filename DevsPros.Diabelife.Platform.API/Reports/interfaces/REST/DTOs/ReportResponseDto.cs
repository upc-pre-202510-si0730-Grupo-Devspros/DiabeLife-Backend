namespace DevsPros.Diabelife.Platform.API.Reports.Interfaces.REST.DTOs;

public class ReportResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public string Type { get; set; } = string.Empty;
    public string Data { get; set; } = string.Empty;
    public bool Selected { get; set; }
    public bool Shared { get; set; }
    public int UserId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}