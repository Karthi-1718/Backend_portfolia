using Microsoft.AspNetCore.Mvc;
using PortfolioApi.Interfaces;
using PortfolioApi.Models;

namespace PortfolioApi.Controllers;

[ApiController]
[Route("api/portfolio")]
public class PortfolioController : ControllerBase
{
    private readonly IPortfolioService _service;

    public PortfolioController(IPortfolioService service) => _service = service;

    // GET /api/portfolio/profile
    [HttpGet("profile")]
    public async Task<ActionResult<Profile>> GetProfile(CancellationToken cancellationToken)
    {
        var profile = await _service.GetProfileAsync(cancellationToken);
        return profile is null
            ? NotFound(new ProblemDetails { Status = 404, Title = "No profile found. Run Database/PortfolioDB.sql first." })
            : Ok(profile);
    }

    // GET /api/portfolio/skills
    [HttpGet("skills")]
    public async Task<ActionResult<IReadOnlyList<Skill>>> GetSkills(CancellationToken cancellationToken)
        => Ok(await _service.GetSkillsAsync(cancellationToken));

    // GET /api/portfolio/projects
    [HttpGet("projects")]
    public async Task<ActionResult<IReadOnlyList<Project>>> GetProjects(CancellationToken cancellationToken)
        => Ok(await _service.GetProjectsAsync(cancellationToken));

    // GET /api/portfolio/education
    [HttpGet("education")]
    public async Task<ActionResult<IReadOnlyList<Education>>> GetEducation(CancellationToken cancellationToken)
        => Ok(await _service.GetEducationAsync(cancellationToken));

    // GET /api/portfolio/certifications
    [HttpGet("certifications")]
    public async Task<ActionResult<IReadOnlyList<Certification>>> GetCertifications(CancellationToken cancellationToken)
        => Ok(await _service.GetCertificationsAsync(cancellationToken));
}
