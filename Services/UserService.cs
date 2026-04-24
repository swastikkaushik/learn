public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;

    public UserService(IUserRepository userRepository, IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<List<User>> GetAllAsync()
    {
        return await _userRepository.GetAllAsync();
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        return await _userRepository.GetByIdAsync(id);
    }

    public async Task<User> RegisterAsync(RegisterRequest request)
    {
        var existing = await _userRepository.GetByEmailAsync(request.Email.Trim().ToLower());
        if (existing is not null)
        {
            throw new InvalidOperationException("Email already exists.");
        }

        var user = new User
        {
            Name = request.Name.Trim(),
            Email = request.Email.Trim().ToLowerInvariant(),
            Password = _passwordHasher.Hash(request.Password),
            Role = string.IsNullOrWhiteSpace(request.Role) ? "User" : request.Role.Trim(),
            CreatedAt = DateTime.UtcNow
        };

        return await _userRepository.AddAsync(user);
    }
}
