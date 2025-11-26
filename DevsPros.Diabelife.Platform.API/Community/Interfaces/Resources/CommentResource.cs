namespace DevsPros.Diabelife.Platform.API.Community.Interfaces.Resources;

public record CommentResource(
    Guid Id,
    Guid PostId,
    Guid AuthorId,
    string AuthorName,
    string Content,
    DateTime CreatedAt
);