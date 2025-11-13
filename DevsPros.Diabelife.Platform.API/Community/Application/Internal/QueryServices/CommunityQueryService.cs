using DevsPros.Diabelife.Platform.API.Community.Domain.Model.Entities;
using DevsPros.Diabelife.Platform.API.Community.Domain.Model.Queries;
using DevsPros.Diabelife.Platform.API.Community.Domain.Repositories;
using DevsPros.Diabelife.Platform.API.Community.Domain.Services;
using DevsPros.Diabelife.Platform.API.Community.Domain.Model.ValueObjects;
using CommunityPost = DevsPros.Diabelife.Platform.API.Community.Domain.Model.Aggregates.CommunityPost;

namespace DevsPros.Diabelife.Platform.API.Community.Application.Internal.QueryServices;

public class CommunityQueryService(
    ICommunityPostRepository postRepository,
    ICommentRepository commentRepository)
    : ICommunityQueryService
{
    public async Task<IEnumerable<CommunityPost>> HandleAsync(GetAllPostsQuery query)
        => await postRepository.ListAsync();

    public async Task<CommunityPost?> HandleAsync(GetPostByIdQuery query)
    {
        var postId = new CommunityPostId(query.PostId); // ✅ conversión Guid -> VO
        return await postRepository.FindByIdAsync(postId);
    }

    public async Task<IEnumerable<CommunityPost>> HandleAsync(GetPostsByAuthorIdQuery query)
    {
        var authorId = new AuthorId(query.AuthorId); // ✅ conversión Guid -> VO
        return await postRepository.FindByAuthorIdAsync(authorId);
    }

    public async Task<IEnumerable<Comment>> HandleAsync(GetCommentsByPostIdQuery query)
    {
        var postId = new CommunityPostId(query.PostId); // ✅ conversión Guid -> VO
        return await commentRepository.FindByPostIdAsync(postId);
    }

    public async Task<IEnumerable<CommunityPost>> HandleAsync(SearchPostsByContentQuery query)
        => await postRepository.SearchByContentAsync(query.Content);
}