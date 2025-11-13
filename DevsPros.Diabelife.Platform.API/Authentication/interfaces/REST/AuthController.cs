using DevsPros.Diabelife.Platform.API.Authentication.Application.Internal.CommandServices;
using DevsPros.Diabelife.Platform.API.Authentication.Application.Internal.QueryServices;
using DevsPros.Diabelife.Platform.API.Authentication.Interfaces.REST.DTOs;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace DevsPros.Diabelife.Platform.API.Authentication.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
[SwaggerTag("Authentication Management")]
public class AuthController : ControllerBase
{
    private readonly IAuthCommandService _authCommandService;
    private readonly IAuthQueryService _authQueryService;

    public AuthController(IAuthCommandService authCommandService, IAuthQueryService authQueryService)
    {
        _authCommandService = authCommandService;
        _authQueryService = authQueryService;
    }

    [HttpPost("register")]
    [SwaggerOperation(
        Summary = "Register a new user",
        Description = "Creates a new user account with email and password")]
    [SwaggerResponse(201, "User registered successfully")]
    [SwaggerResponse(400, "Invalid request data")]
    [SwaggerResponse(409, "User with this email already exists")]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDto request)
    {
        try
        {
            await _authCommandService.RegisterAsync(request);
            return CreatedAtAction(nameof(Register), new { message = "User registered successfully" });
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost("login")]
    [SwaggerOperation(
        Summary = "Login user",
        Description = "Authenticates user and returns JWT token")]
    [SwaggerResponse(200, "Login successful", typeof(LoginResponseDto))]
    [SwaggerResponse(401, "Invalid credentials")]
    [SwaggerResponse(400, "Invalid request data")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
    {
        try
        {
            var token = await _authQueryService.LoginAsync(request);
            var response = new LoginResponseDto
            {
                Token = token,
                Email = request.Email
            };
            return Ok(response);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}