using DevsPros.Diabelife.Platform.API.Community.Domain.Model.ValueObjects;

namespace DevsPros.Diabelife.Platform.API.Community.Domain.Model.Entities;

public class Comment
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public AuthorId AuthorId { get; private set; }
    public Content Content { get; private set; }
    public CommunityPostId PostId { get; private set; }  // <-- relación con el post
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

    protected Comment() { }

    public Comment(AuthorId authorId, Content content, CommunityPostId postId)
    {
        AuthorId = authorId;
        Content = content;
        PostId = postId;
    }
}