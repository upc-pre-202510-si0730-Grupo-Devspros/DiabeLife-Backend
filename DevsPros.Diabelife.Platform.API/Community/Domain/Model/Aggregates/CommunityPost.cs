using DevsPros.Diabelife.Platform.API.Community.Domain.Model.Commands;
using DevsPros.Diabelife.Platform.API.Community.Domain.Model.Entities;
using DevsPros.Diabelife.Platform.API.Community.Domain.Model.Events;
using DevsPros.Diabelife.Platform.API.Community.Domain.Model.ValueObjects;

namespace DevsPros.Diabelife.Platform.API.Community.Domain.Model.Aggregates;

public class CommunityPost
{
    public CommunityPostId Id { get; private set; } = new(Guid.NewGuid());
    public AuthorId AuthorId { get; private set; }
    public AuthorName AuthorName { get; private set; }

    public Content Content { get; private set; }
    public ImageUrl? ImageUrl { get; private set; }
    public int Likes { get; private set; }
    public List<Comment> Comments { get; private set; } = new();

    protected CommunityPost() { }

    public CommunityPost(AuthorId authorId, AuthorName authorName, Content content, ImageUrl? imageUrl = null)
    {
        AuthorId = authorId;
        AuthorName = authorName;
        Content = content;
        ImageUrl = imageUrl;
    }

    public CommunityPost(CreatePostCommand command)
        : this(
                new AuthorId(command.AuthorId),
                new AuthorName(command.AuthorName),
                new Content(command.Content),
                string.IsNullOrWhiteSpace(command.ImageUrl) ? null : new ImageUrl(command.ImageUrl))
        
    {
        AddDomainEvent(new PostCreatedEvent(AuthorId.Value, Content.Value, ImageUrl?.Value));

    }

    public void AddComment(AuthorId authorId, AuthorName authorName, Content content)
    {
        var comment = new Comment(authorId, authorName, content, Id);
        Comments.Add(comment);

        AddDomainEvent(new CommentAddedEvent(
            Id.Value,
            authorId.Value,
            content.Value
        ));
    }

    public void AddLike(AuthorId authorId)
    {
        Likes++;
        AddDomainEvent(new PostLikedEvent(Id.Value, authorId.Value));
    }

    private readonly List<object> _domainEvents = new();
    public IReadOnlyCollection<object> DomainEvents => _domainEvents.AsReadOnly();

    protected void AddDomainEvent(object @event) => _domainEvents.Add(@event);
}
