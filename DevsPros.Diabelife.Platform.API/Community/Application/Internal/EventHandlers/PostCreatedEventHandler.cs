using DevsPros.Diabelife.Platform.API.Community.Domain.Model.Events;
using DevsPros.Diabelife.Platform.API.Shared.Application.Internal.EventHandlers;

namespace DevsPros.Diabelife.Platform.API.Community.Application.Internal.EventHandlers

{
    public class PostCreatedEventHandler : IEventHandler<PostCreatedEvent>
    {
        public Task Handle(PostCreatedEvent domainEvent, CancellationToken cancellationToken)
        {
            return On(domainEvent);
        }

        private static Task On(PostCreatedEvent domainEvent)
        {
            Console.WriteLine("Post created whit  AuthorId: {0}, content: {1}, image: {2}", 
                domainEvent.AuthorId, domainEvent.Content, domainEvent.ImageUrl ?? "not image ");
            return Task.CompletedTask;
        }
    }
}