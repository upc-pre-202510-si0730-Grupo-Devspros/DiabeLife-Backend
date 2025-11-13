using DevsPros.Diabelife.Platform.API.Authentication.Interfaces.REST.DTOs;

namespace DevsPros.Diabelife.Platform.API.Authentication.Application.Internal.QueryServices;

public interface IAuthQueryService
{
    Task<string> LoginAsync(LoginRequestDto request);
}