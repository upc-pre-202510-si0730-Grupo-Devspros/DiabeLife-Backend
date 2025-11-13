namespace DevsPros.Diabelife.Platform.API.Community.Interfaces.Resources;

public record CommentResource(
    Guid Id,
    Guid PostId,
    Guid AuthorId,
    string Content,
    DateTime CreatedAt
);