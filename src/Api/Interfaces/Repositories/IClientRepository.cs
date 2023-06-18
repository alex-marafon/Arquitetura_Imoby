using Custom_Architecture.Entities;

namespace Custom_Architecture.Interfaces.Repositories;

public interface IClientRepository
{
    Task<IEnumerable<Client>> GetAllAsync();

    Task<Client> GetByIdAsync(int clientId);

    Task<Client> CreateAsync(Client client);

    Task<Client> UpdateAsync(Client client);
}
