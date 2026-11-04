using PortfolioApi.Interfaces;
using PortfolioApi.Models;

namespace PortfolioApi.Services;

public class PortfolioService : IPortfolioService
{
    private readonly IPortfolioRepository _repository;

    public PortfolioService(IPortfolioRepository repository) => _repository = repository;

    public Task<Profile?> GetProfileAsync(CancellationToken cancellationToken)
        => _repository.GetProfileAsync(cancellationToken);

    public Task<IReadOnlyList<Skill>> GetSkillsAsync(CancellationToken cancellationToken)
        => _repository.GetSkillsAsync(cancellationToken);

    public Task<IReadOnlyList<Project>> GetProjectsAsync(CancellationToken cancellationToken)
        => _repository.GetProjectsAsync(cancellationToken);

    public Task<IReadOnlyList<Education>> GetEducationAsync(CancellationToken cancellationToken)
        => _repository.GetEducationAsync(cancellationToken);

    public Task<IReadOnlyList<Certification>> GetCertificationsAsync(CancellationToken cancellationToken)
        => _repository.GetCertificationsAsync(cancellationToken);
}
