public interface IAuthService
{
    Task<AuthResponse?> LoginAsync(LoginRequest request);
}
