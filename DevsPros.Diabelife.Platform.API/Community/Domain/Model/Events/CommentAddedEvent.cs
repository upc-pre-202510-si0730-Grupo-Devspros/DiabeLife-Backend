using DevsPros.Diabelife.Platform.API.Shared.Domain.Model.Events;

namespace DevsPros.Diabelife.Platform.API.Community.Domain.Model.Events;

public record CommentAddedEvent(Guid PostId, Guid AuthorId, string Content);
