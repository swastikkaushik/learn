using Microsoft.EntityFrameworkCore;

public class OrderRepository : IOrderRepository
{
    private readonly AppDbContext _dbContext;

    public OrderRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Order>> GetByUserIdAsync(int userId)
    {
        return await _dbContext.Orders
            .Include(x => x.Product)
            .Where(x => x.UserId == userId)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
    }

    public async Task<Order> AddAsync(Order order)
    {
        await _dbContext.Orders.AddAsync(order);
        await _dbContext.SaveChangesAsync();
        return order;
    }
}
