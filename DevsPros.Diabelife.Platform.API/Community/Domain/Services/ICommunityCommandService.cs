using DevsPros.Diabelife.Platform.API.Community.Domain.Model.Aggregates;
using DevsPros.Diabelife.Platform.API.Community.Domain.Model.Commands;

namespace DevsPros.Diabelife.Platform.API.Community.Domain.Services;


public interface ICommunityCommandService
{
    Task<CommunityPost?> Handle(CreatePostCommand command);
    Task<CommunityPost?> Handle(AddCommentCommand command);
    Task<CommunityPost?> Handle(AddLikeCommand command);
    Task<bool> Handle(DeletePostCommand command);
}