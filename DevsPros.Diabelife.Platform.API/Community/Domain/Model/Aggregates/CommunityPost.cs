using DevsPros.Diabelife.Platform.API.Community.Domain.Model.Commands;
using DevsPros.Diabelife.Platform.API.Community.Domain.Model.Entities;
using DevsPros.Diabelife.Platform.API.Community.Domain.Model.Events;
using DevsPros.Diabelife.Platform.API.Community.Domain.Model.ValueObjects;

namespace DevsPros.Diabelife.Platform.API.Community.Domain.Model.Aggregates;

public class CommunityPost
{
    public CommunityPostId Id { get; private set; } = new(Guid.NewGuid());
    public AuthorId AuthorId { get; private set; }
    public Content Content { get; private set; }
    public ImageUrl? ImageUrl { get; private set; }
    public int Likes { get; private set; }
    public List<Comment> Comments { get; private set; } = new();

    // Constructor protegido (EF o serialización)
    protected CommunityPost() { }

    // Constructor principal
    public CommunityPost(AuthorId authorId, Content content, ImageUrl? imageUrl = null)
    {
        AuthorId = authorId;
        Content = content;
        ImageUrl = imageUrl;
    }

    // Desde Command
    public CommunityPost(CreatePostCommand command)
        : this(new AuthorId(command.AuthorId), new Content(command.Content),
            string.IsNullOrWhiteSpace(command.ImageUrl) ? null : new ImageUrl(command.ImageUrl))
    {
        AddDomainEvent(new PostCreatedEvent(AuthorId.Value, Content.Value, ImageUrl?.Value));
    }

    // Agregar comentario
    public void AddComment(AuthorId authorId, Content content)
    {
        var comment = new Comment(authorId, content, Id);
        Comments.Add(comment);
        AddDomainEvent(new CommentAddedEvent(Id.Value, authorId.Value, content.Value));
    }

    // Dar like
    public void AddLike(AuthorId authorId)
    {
        Likes++;
        AddDomainEvent(new PostLikedEvent(Id.Value, authorId.Value));
    }

    // Eventos de dominio
    private readonly List<object> _domainEvents = new();
    public IReadOnlyCollection<object> DomainEvents => _domainEvents.AsReadOnly();

    protected void AddDomainEvent(object @event) => _domainEvents.Add(@event);
}