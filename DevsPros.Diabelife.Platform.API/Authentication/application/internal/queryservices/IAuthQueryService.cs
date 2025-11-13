using DevsPros.Diabelife.Platform.API.Authentication.Interfaces.REST.DTOs;
using DevsPros.Diabelife.Platform.API.Shared.Domain.Model;

namespace DevsPros.Diabelife.Platform.API.Authentication.Application.Internal.QueryServices;

public interface IAuthQueryService
{
    Task<(string token, User user)> LoginAsync(LoginRequestDto request);
}