using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IAuthService _authService;

    public AuthController(IUserService userService, IAuthService authService)
    {
        _userService = userService;
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var user = await _userService.RegisterAsync(request);
        return Ok(new
        {
            user.Id,
            user.Name,
            user.Email,
            user.Role,
            user.CreatedAt
        });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var response = await _authService.LoginAsync(request);
        if (response == null)
        {
            return Unauthorized(new { error = "Invalid email or password." });
        }

        return Ok(response);
    }
}
