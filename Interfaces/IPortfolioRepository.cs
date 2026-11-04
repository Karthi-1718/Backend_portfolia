using PortfolioApi.Models;

namespace PortfolioApi.Interfaces;

// The data-access layer: only this layer talks to SQL Server.
public interface IPortfolioRepository
{
    Task<Profile?> GetProfileAsync(CancellationToken cancellationToken);
    Task<IReadOnlyList<Skill>> GetSkillsAsync(CancellationToken cancellationToken);
    Task<IReadOnlyList<Project>> GetProjectsAsync(CancellationToken cancellationToken);
    Task<IReadOnlyList<Education>> GetEducationAsync(CancellationToken cancellationToken);
    Task<IReadOnlyList<Certification>> GetCertificationsAsync(CancellationToken cancellationToken);
}
