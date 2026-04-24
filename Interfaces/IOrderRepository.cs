public interface IOrderRepository
{
    Task<List<Order>> GetByUserIdAsync(int userId);
    Task<Order> AddAsync(Order order);
}
