using Custom_Architecture.Entities;

namespace Custom_Architecture.Interfaces.Services;

public interface IClientService
{
    Task<IEnumerable<Client>> GetAllAsync();

    Task<Client?> GetByIdAsync(int clientId);

    Task<Client?> CreateAsync(Client client);

    Task<Client?> UpdateAsync(Client client);
}
