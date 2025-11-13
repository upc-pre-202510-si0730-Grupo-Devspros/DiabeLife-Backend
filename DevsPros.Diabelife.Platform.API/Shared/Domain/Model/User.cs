using DevsPros.Diabelife.Platform.API.Reports.Domain.Model;

namespace DevsPros.Diabelife.Platform.API.Shared.Domain.Model;

public class User : BaseEntity
{
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    
    // Navigation property for Reports
    public virtual ICollection<Report> Reports { get; set; } = new List<Report>();
}