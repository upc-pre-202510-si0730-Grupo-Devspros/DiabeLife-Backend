using DevsPros.Diabelife.Platform.API.Community.Domain.Model.Commands;
using DevsPros.Diabelife.Platform.API.Community.Interfaces.Resources;

namespace DevsPros.Diabelife.Platform.API.Community.Interfaces.Transform;


public static class LikePostCommandFromResourceAssembler
{
    public static AddLikeCommand ToCommandFromResource(Guid postId, LikePostResource resource)
    {
        return new AddLikeCommand(postId, resource.AuthorId);
    }
}