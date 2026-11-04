using PortfolioApi.Models;

namespace PortfolioApi.Interfaces;

// The business layer: controllers talk to services, services talk to repositories.
public interface IPortfolioService
{
    Task<Profile?> GetProfileAsync(CancellationToken cancellationToken);
    Task<IReadOnlyList<Skill>> GetSkillsAsync(CancellationToken cancellationToken);
    Task<IReadOnlyList<Project>> GetProjectsAsync(CancellationToken cancellationToken);
    Task<IReadOnlyList<Education>> GetEducationAsync(CancellationToken cancellationToken);
    Task<IReadOnlyList<Certification>> GetCertificationsAsync(CancellationToken cancellationToken);
}
