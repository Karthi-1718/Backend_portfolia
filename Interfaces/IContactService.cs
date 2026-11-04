using PortfolioApi.Models;

namespace PortfolioApi.Interfaces;

public interface IContactService
{
    Task<int> SubmitAsync(ContactRequest request, CancellationToken cancellationToken);
}
