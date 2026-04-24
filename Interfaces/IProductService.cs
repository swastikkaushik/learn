public interface IProductService
{
    Task<List<Product>> GetAllAsync();
    Task<Product> CreateAsync(CreateProductRequest request);
}
