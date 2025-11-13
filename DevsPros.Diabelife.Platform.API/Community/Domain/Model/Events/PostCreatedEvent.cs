using DevsPros.Diabelife.Platform.API.Shared.Domain.Model.Events;

namespace DevsPros.Diabelife.Platform.API.Community.Domain.Model.Events;

public record PostCreatedEvent(Guid AuthorId, string Content, string? ImageUrl) : IEvent;
