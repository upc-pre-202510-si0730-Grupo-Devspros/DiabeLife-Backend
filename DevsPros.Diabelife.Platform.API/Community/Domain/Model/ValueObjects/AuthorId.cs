namespace DevsPros.Diabelife.Platform.API.Community.Domain.Model.ValueObjects;

public record AuthorId(Guid Value)
{
    public AuthorId(int value) : this(Guid.NewGuid()) { } 
}