public interface IOrderService
{
    Task<List<Order>> GetByUserAsync(int userId);
    Task<Order> CreateAsync(int userId, CreateOrderRequest request);
}
