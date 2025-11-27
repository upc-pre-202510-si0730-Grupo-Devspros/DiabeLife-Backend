namespace DevsPros.Diabelife.Platform.API.Community.Interfaces.Resources;

public record CommunityPostResource(
    Guid Id,
    string Content,
    Guid AuthorId,
    string AuthorName,
    string? ImageUrl,
    int Likes,
    int CommentCount,
    DateTime CreatedAt
);
