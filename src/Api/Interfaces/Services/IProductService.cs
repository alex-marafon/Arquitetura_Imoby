using Custom_Architecture.Entities;

namespace Custom_Architecture.Interfaces.Services;

public interface IProductService
{
    Task<IEnumerable<Product>> GetAllAsync();

    Task<Product?> GetByIdAsync(int productId);

    Task<Product?> CreateAsync(Product product);

    Task<Product?> UpdateAsync(Product product);
}