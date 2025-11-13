namespace DevsPros.Diabelife.Platform.API.Community.Interfaces.Resources;

public record CommunityPostResource(
    Guid Id,
    string Content,
    Guid AuthorId,
    string? ImageUrl,
    int Likes,
    int CommentsCount
);