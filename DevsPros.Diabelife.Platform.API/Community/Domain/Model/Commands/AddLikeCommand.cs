namespace DevsPros.Diabelife.Platform.API.Community.Domain.Model.Commands;

public record AddLikeCommand(Guid PostId, Guid AuthorId);
