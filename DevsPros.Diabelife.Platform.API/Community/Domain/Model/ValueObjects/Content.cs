namespace DevsPros.Diabelife.Platform.API.Community.Domain.Model.ValueObjects;

public record Content
{
    public string Value { get; }

    public Content(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Content cannot be empty");

        Value = value.Trim();
    }

    public override string ToString() => Value;
}