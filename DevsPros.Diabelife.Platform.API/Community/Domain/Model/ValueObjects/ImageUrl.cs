namespace DevsPros.Diabelife.Platform.API.Community.Domain.Model.ValueObjects;

public record ImageUrl(string? Value)
{
    public bool HasImage => !string.IsNullOrEmpty(Value);
}