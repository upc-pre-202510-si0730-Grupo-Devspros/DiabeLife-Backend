namespace DevsPros.Diabelife.Platform.API.Community.Domain.Model.Commands;

public record CreatePostCommand(Guid AuthorId, string Content, string? ImageUrl = null);
