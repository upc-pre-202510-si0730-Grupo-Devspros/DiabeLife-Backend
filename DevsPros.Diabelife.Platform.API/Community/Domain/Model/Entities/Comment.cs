using DevsPros.Diabelife.Platform.API.Community.Domain.Model.ValueObjects;

namespace DevsPros.Diabelife.Platform.API.Community.Domain.Model.Entities;

public class Comment
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public CommunityPostId PostId { get; private set; }
    public AuthorId AuthorId { get; private set; }
    public AuthorName AuthorName { get; private set; }
    public Content Content { get; private set; }
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

    // EF constructor
    protected Comment() {}

    public Comment(AuthorId authorId, AuthorName authorName, Content content, CommunityPostId postId)
    {
        AuthorId = authorId;
        AuthorName = authorName;
        Content = content;
        PostId = postId;
    }
}