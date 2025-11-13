namespace DevsPros.Diabelife.Platform.API.Community.Domain.Model.ValueObjects;

public record Content
{
    public string Value { get; }

    public Content(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("The value cannot be null or whitespace.");
        
        Value = value;
    }
}