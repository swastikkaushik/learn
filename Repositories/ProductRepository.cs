using Microsoft.EntityFrameworkCore;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _dbContext;

    public ProductRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Product?> GetByIdAsync(int id)
    {
        return await _dbContext.Products.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<Product>> GetAllAsync()
    {
        return await _dbContext.Products.OrderByDescending(x => x.CreatedAt).ToListAsync();
    }

    public async Task<Product> AddAsync(Product product)
    {
        await _dbContext.Products.AddAsync(product);
        await _dbContext.SaveChangesAsync();
        return product;
    }

    public async Task UpdateAsync(Product product)
    {
        _dbContext.Products.Update(product);
        await _dbContext.SaveChangesAsync();
    }
}
