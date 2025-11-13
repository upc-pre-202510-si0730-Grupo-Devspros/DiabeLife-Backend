using DevsPros.Diabelife.Platform.API.Community.Domain.Model.Entities;
using DevsPros.Diabelife.Platform.API.Community.Interfaces.Resources;

namespace DevsPros.Diabelife.Platform.API.Community.Interfaces.Transform;

public static class CommentResourceFromEntityAssembler
{
    public static CommentResource ToResourceFromEntity(Comment entity)
    {
        return new CommentResource(
            entity.Id,
            entity.PostId.Value,      
            entity.AuthorId.Value,
            entity.Content.Value,
            entity.CreatedAt
        );
    }
}