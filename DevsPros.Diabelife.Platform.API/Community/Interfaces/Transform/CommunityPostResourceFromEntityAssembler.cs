using DevsPros.Diabelife.Platform.API.Community.Domain.Model.Aggregates;
using DevsPros.Diabelife.Platform.API.Community.Interfaces.Resources;

namespace DevsPros.Diabelife.Platform.API.Community.Interfaces.Transform;

public static class CommunityPostResourceFromEntityAssembler
{
    public static CommunityPostResource ToResourceFromEntity(CommunityPost entity)
    {
        return new CommunityPostResource(
            entity.Id.Value,
            entity.Content.Value,
            entity.AuthorId.Value,
            entity.ImageUrl?.Value,
            entity.Likes,
            entity.Comments.Count
        );
    }
}