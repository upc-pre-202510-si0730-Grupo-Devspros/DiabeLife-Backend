namespace DevsPros.Diabelife.Platform.API.Community.Domain.Model.Commands;
public record AddCommentCommand(Guid PostId, Guid  AuthorId, string Content);
