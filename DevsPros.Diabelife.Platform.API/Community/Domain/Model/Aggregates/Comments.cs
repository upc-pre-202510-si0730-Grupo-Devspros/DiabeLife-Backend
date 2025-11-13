using DevsPros.Diabelife.Platform.API.Community.Domain.Model.Commands;
using DevsPros.Diabelife.Platform.API.Community.Domain.Model.Entities;
using DevsPros.Diabelife.Platform.API.Community.Domain.Model.ValueObjects;

namespace DevsPros.Diabelife.Platform.API.Community.Domain.Model.Aggregates;

public partial class Comments
{
    private readonly List<Comment> _comments = new();
    public IReadOnlyCollection<Comment> All => _comments.AsReadOnly();

    public void Add(AddCommentCommand command, CommunityPostId postId)
    {
        var comment = new Comment(
            new AuthorId(command.AuthorId),
            new Content(command.Content),
            postId); 
        _comments.Add(comment);
    }
}