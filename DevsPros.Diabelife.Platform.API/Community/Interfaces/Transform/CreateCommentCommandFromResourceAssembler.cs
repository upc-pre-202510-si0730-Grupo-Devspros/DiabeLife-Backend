using DevsPros.Diabelife.Platform.API.Community.Domain.Model.Commands;
using DevsPros.Diabelife.Platform.API.Community.Interfaces.Resources;

namespace DevsPros.Diabelife.Platform.API.Community.Interfaces.Transform;

public static class CreateCommentCommandFromResourceAssembler
{
    public static AddCommentCommand ToCommandFromResource(CreateCommentResource resource)
    {
        return new AddCommentCommand(
            resource.PostId,
            resource.AuthorId,
            resource.Content
        );
    }
}