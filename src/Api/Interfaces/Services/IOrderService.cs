using Custom_Architecture.Entities;

namespace Custom_Architecture.Interfaces.Services;

public interface IOrderService 
{
    Task<IEnumerable<Order>> GetAllAsync();

    Task<Order?> GetByIdAsync(int orderId);

    Task<Order?> CreateAsync(Order order);
}