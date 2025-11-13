using DevsPros.Diabelife.Platform.API.Community.Domain.Model.Entities;
using DevsPros.Diabelife.Platform.API.Community.Domain.Model.Queries;

namespace DevsPros.Diabelife.Platform.API.Community.Application.Internal.QueryServices;

public interface ICommentQueryService
{
    Task<IEnumerable<Comment>> Handle(GetCommentsByPostIdQuery query);
}