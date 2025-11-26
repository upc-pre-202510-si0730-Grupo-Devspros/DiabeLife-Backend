namespace DevsPros.Diabelife.Platform.API.Community.Interfaces.Resources;

public record CreateCommunityPostResource(
    Guid AuthorId,
    string AuthorName,
    string Content,
    string? ImageUrl
);