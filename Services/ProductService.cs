public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<List<Product>> GetAllAsync()
    {
        return await _productRepository.GetAllAsync();
    }

    public async Task<Product> CreateAsync(CreateProductRequest request)
    {
        if (request.Price <= 0) throw new InvalidOperationException("Price must be greater than 0.");
        if (request.Stock < 0) throw new InvalidOperationException("Stock cannot be negative.");

        var product = new Product
        {
            Name = request.Name.Trim(),
            Description = request.Description.Trim(),
            Price = request.Price,
            Stock = request.Stock,
            CreatedAt = DateTime.UtcNow
        };

        return await _productRepository.AddAsync(product);
    }
}
