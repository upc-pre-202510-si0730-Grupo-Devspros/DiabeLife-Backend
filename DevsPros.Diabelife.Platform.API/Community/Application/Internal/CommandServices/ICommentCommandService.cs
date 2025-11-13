using DevsPros.Diabelife.Platform.API.Community.Domain.Model.Commands;
using DevsPros.Diabelife.Platform.API.Community.Domain.Model.Entities;

namespace DevsPros.Diabelife.Platform.API.Community.Application.Internal.CommandServices;

public interface ICommentCommandService
{
    Task<Comment?> Handle(AddCommentCommand command);
}