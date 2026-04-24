public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;

    public OrderService(IOrderRepository orderRepository, IProductRepository productRepository)
    {
        _orderRepository = orderRepository;
        _productRepository = productRepository;
    }

    public async Task<List<Order>> GetByUserAsync(int userId)
    {
        return await _orderRepository.GetByUserIdAsync(userId);
    }

    public async Task<Order> CreateAsync(int userId, CreateOrderRequest request)
    {
        if (request.Quantity <= 0) throw new InvalidOperationException("Quantity must be greater than 0.");

        var product = await _productRepository.GetByIdAsync(request.ProductId)
                      ?? throw new KeyNotFoundException("Product not found.");

        if (product.Stock < request.Quantity)
        {
            throw new InvalidOperationException("Not enough stock.");
        }

        product.Stock -= request.Quantity;
        await _productRepository.UpdateAsync(product);

        var order = new Order
        {
            UserId = userId,
            ProductId = product.Id,
            Quantity = request.Quantity,
            TotalPrice = product.Price * request.Quantity,
            CreatedAt = DateTime.UtcNow
        };

        return await _orderRepository.AddAsync(order);
    }
}
