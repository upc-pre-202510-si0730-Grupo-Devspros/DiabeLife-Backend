using DevsPros.Diabelife.Platform.API.Community.Domain.Model.Aggregates;
using DevsPros.Diabelife.Platform.API.Community.Domain.Model.Commands;
using DevsPros.Diabelife.Platform.API.Community.Domain.Model.ValueObjects;
using DevsPros.Diabelife.Platform.API.Community.Domain.Repositories;
using DevsPros.Diabelife.Platform.API.Community.Domain.Services;
using DevsPros.Diabelife.Platform.API.Shared.Domain.Repositories;

namespace DevsPros.Diabelife.Platform.API.Community.Application.Internal.CommandServices;

public class CommunityCommandService(
    ICommunityPostRepository postRepository,
    ICommentRepository commentRepository,
    IUnitOfWork unitOfWork)
    : ICommunityCommandService
{
    public async Task<CommunityPost?> Handle(CreatePostCommand command)
    {
        var post = new CommunityPost(command);
        await postRepository.AddAsync(post);
        await unitOfWork.CompleteAsync();
        return post;
    }

    public async Task<CommunityPost?> Handle(AddCommentCommand command)
    {
        var postId = new CommunityPostId(command.PostId);
        var post = await postRepository.FindByIdAsync(postId);
        if (post is null)
            throw new KeyNotFoundException("El post no fue encontrado.");

        post.AddComment(new AuthorId(command.AuthorId), new Content(command.Content));
        await unitOfWork.CompleteAsync();
        return post;
    }

    public async Task<CommunityPost?> Handle(AddLikeCommand command)
    {
        var postId = new CommunityPostId(command.PostId);
        var post = await postRepository.FindByIdAsync(postId);
        if (post is null)
            throw new KeyNotFoundException("El post no fue encontrado.");

        post.AddLike(new AuthorId(command.AuthorId));
        await unitOfWork.CompleteAsync();
        return post;
    }

    public async Task<bool> Handle(DeletePostCommand command)
    {
        var postId = new CommunityPostId(command.PostId);
        var post = await postRepository.FindByIdAsync(postId);
        if (post is null) return false;

        postRepository.Remove(post);
        await unitOfWork.CompleteAsync();
        return true;
    }
}
