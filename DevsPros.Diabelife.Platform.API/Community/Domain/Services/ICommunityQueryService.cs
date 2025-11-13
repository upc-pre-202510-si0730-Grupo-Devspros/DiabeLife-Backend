using DevsPros.Diabelife.Platform.API.Community.Domain.Model.Aggregates;
using DevsPros.Diabelife.Platform.API.Community.Domain.Model.Entities;
using DevsPros.Diabelife.Platform.API.Community.Domain.Model.Queries;
using CommunityPost = DevsPros.Diabelife.Platform.API.Community.Domain.Model.Aggregates.CommunityPost;

namespace DevsPros.Diabelife.Platform.API.Community.Domain.Services
{
    public interface ICommunityQueryService
    {
        Task<IEnumerable<CommunityPost>> HandleAsync(GetAllPostsQuery query);
        Task<CommunityPost?> HandleAsync(GetPostByIdQuery query);
        Task<IEnumerable<CommunityPost>> HandleAsync(GetPostsByAuthorIdQuery query);
        Task<IEnumerable<Comment>> HandleAsync(GetCommentsByPostIdQuery query);
        Task<IEnumerable<CommunityPost>> HandleAsync(SearchPostsByContentQuery query);
    }
}