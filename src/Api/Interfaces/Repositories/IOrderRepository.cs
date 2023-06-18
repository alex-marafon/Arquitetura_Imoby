using Custom_Architecture.Entities;

namespace Custom_Architecture.Interfaces.Repositories;

public interface IOrderRepository
{
    Task<IEnumerable<Order>> GetAllAsync();

    Task<Order> GetByIdAsync(int orderId);

    Task<Order> CreateAsync(Order order);
}
