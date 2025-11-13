using DevsPros.Diabelife.Platform.API.Shared.Domain.Model;

namespace DevsPros.Diabelife.Platform.API.Reports.Domain.Model;

public class Report : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public string Type { get; set; } = string.Empty;
    public string Data { get; set; } = string.Empty;
    public bool Selected { get; set; }
    public bool Shared { get; set; }
    
    // Foreign Key
    public int UserId { get; set; }
    
    // Navigation property
    public virtual User User { get; set; } = null!;
}