using DevsPros.Diabelife.Platform.API.Community.Domain.Model.Entities;
using DevsPros.Diabelife.Platform.API.Community.Domain.Model.ValueObjects;
using DevsPros.Diabelife.Platform.API.Community.Domain.Repositories;
using DevsPros.Diabelife.Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using DevsPros.Diabelife.Platform.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DevsPros.Diabelife.Platform.API.Community.Infrastructure.Persistence.EFC.Repositories;

public class CommentRepository : BaseRepository<Comment, Guid>, ICommentRepository
{
    public CommentRepository(AppDbContext context) : base(context) { }

    public async Task<IEnumerable<Comment>> FindByPostIdAsync(CommunityPostId postId)
    {
        var id = postId.Value;
        return await Context.Set<Comment>()
            .Where(c => c.PostId.Value == id)
            .ToListAsync();
    }

    public async Task AddAsync(Comment comment)
    {
        await Context.Set<Comment>().AddAsync(comment);
    }
}