using DevsPros.Diabelife.Platform.API.Community.Domain.Model.Commands;
using DevsPros.Diabelife.Platform.API.Community.Interfaces.Resources;

namespace DevsPros.Diabelife.Platform.API.Community.Interfaces.Transform;

public static class CreateCommunityPostCommandFromResourceAssembler
{
    public static CreatePostCommand ToCommandFromResource(CreateCommunityPostResource resource)
    {
        return new CreatePostCommand(
            resource.AuthorId,
            resource.Content,
            resource.ImageUrl
        );
    }
}