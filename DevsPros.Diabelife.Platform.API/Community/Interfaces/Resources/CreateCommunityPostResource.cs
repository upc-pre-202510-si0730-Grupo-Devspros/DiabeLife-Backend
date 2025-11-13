namespace DevsPros.Diabelife.Platform.API.Community.Interfaces.Resources;

public record CreateCommunityPostResource(
    Guid AuthorId,
    string Content,
    string? ImageUrl
);