using DevsPros.Diabelife.Platform.API.Community.Domain.Model.Aggregates;
using DevsPros.Diabelife.Platform.API.Community.Domain.Model.ValueObjects;

namespace DevsPros.Diabelife.Platform.API.Community.Domain.Repositories;


public interface ICommunityPostRepository
{
    Task<IEnumerable<CommunityPost>> ListAsync();
    Task<CommunityPost?> FindByIdAsync(CommunityPostId id);
    Task<IEnumerable<CommunityPost>> FindByAuthorIdAsync(AuthorId authorId);
    Task<IEnumerable<CommunityPost>> SearchByContentAsync(string content);
    Task AddAsync(CommunityPost post);
    void Remove(CommunityPost post);
}