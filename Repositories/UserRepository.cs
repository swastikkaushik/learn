using Microsoft.EntityFrameworkCore;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _dbContext;

    public UserRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == email);
    }

    public async Task<List<User>> GetAllAsync()
    {
        return await _dbContext.Users.OrderByDescending(x => x.CreatedAt).ToListAsync();
    }

    public async Task<User> AddAsync(User user)
    {
        await _dbContext.Users.AddAsync(user);
        await _dbContext.SaveChangesAsync();
        return user;
    }
}