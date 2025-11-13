using Cortex.Mediator.Notifications;
using DevsPros.Diabelife.Platform.API.Shared.Domain.Model.Events;

namespace DevsPros.Diabelife.Platform.API.Shared.Application.Internal.EventHandlers;

public interface IEventHandler<in TEvent> : INotificationHandler<TEvent> where TEvent : IEvent
{
}