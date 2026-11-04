using PortfolioApi.Models;

namespace PortfolioApi.Interfaces;

public interface IContactRepository
{
    // Saves a message and returns the new row Id.
    Task<int> AddAsync(ContactMessage message, CancellationToken cancellationToken);
}
