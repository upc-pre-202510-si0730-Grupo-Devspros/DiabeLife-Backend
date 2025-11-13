using DevsPros.Diabelife.Platform.API.Community.Domain.Model.Entities;
using DevsPros.Diabelife.Platform.API.Community.Domain.Model.ValueObjects;

namespace DevsPros.Diabelife.Platform.API.Community.Domain.Repositories;

public interface ICommentRepository
{
    Task<IEnumerable<Comment>> FindByPostIdAsync(CommunityPostId postId);
    Task AddAsync(Comment comment);
}