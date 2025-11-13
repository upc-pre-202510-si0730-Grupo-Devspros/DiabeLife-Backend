using DevsPros.Diabelife.Platform.API.Authentication.Interfaces.REST.DTOs;

namespace DevsPros.Diabelife.Platform.API.Authentication.Application.Internal.CommandServices;

public interface IAuthCommandService
{
    Task RegisterAsync(RegisterRequestDto request);
}