using Custom_Architecture.Entities;

namespace Custom_Architecture.Interfaces.Repositories;

public interface IOrderProductRepository
{
    Task CreateAsync(int orderId, IEnumerable<OrderProduct> orderProducts);
}