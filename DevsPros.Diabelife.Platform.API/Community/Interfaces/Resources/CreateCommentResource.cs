namespace DevsPros.Diabelife.Platform.API.Community.Interfaces.Resources;

public record CreateCommentResource(
    Guid PostId,
    Guid AuthorId,
    string Content
);