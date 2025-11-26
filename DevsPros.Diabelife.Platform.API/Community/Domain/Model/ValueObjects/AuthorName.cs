namespace DevsPros.Diabelife.Platform.API.Community.Domain.Model.ValueObjects;

public record AuthorName
{
    public string Value { get; }

    public AuthorName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("AuthorName cannot be empty");

        Value = value.Trim();
    }

    public override string ToString() => Value;
}