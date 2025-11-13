using DevsPros.Diabelife.Platform.API.Community.Domain.Model.ValueObjects;

namespace DevsPros.Diabelife.Platform.API.Community.Domain.Model.Entities;

public class CommunityPost
{
    public CommunityPostId Id { get; private set; }
    public AuthorId AuthorId { get; private set; }
    public Content Content { get; private set; }
    
    public ImageUrl? ImageUrl { get; private set; }
    public DateTime CreatedAt { get; private set; }
    
    private readonly List<Comment> _comments = new();
    public IReadOnlyCollection<Comment> Comments => _comments.AsReadOnly();
    
    public int Likes { get; private set; }

    public CommunityPost(AuthorId authorId, Content content, ImageUrl? imageUrl = null)
    {
        Id = CommunityPostId.NewId();
        AuthorId = authorId;
        Content = content;
        ImageUrl = imageUrl;
        CreatedAt = DateTime.UtcNow;
        Likes = 0;
    }

    public void AddComment(Comment comment)
    {
        _comments.Add(comment);
    }

    public void AddLike()
    {
        Likes++;
    }

    public void RemoveLike()
    {
        if (Likes > 0) Likes--;
    }
}