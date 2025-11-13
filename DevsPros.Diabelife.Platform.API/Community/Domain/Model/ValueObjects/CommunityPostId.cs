namespace DevsPros.Diabelife.Platform.API.Community.Domain.Model.ValueObjects;

public record CommunityPostId(Guid Value)
{
    public static CommunityPostId NewId() => new(Guid.NewGuid());
}