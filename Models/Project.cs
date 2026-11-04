namespace PortfolioApi.Models;

public class Project
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string TechStack { get; set; } = string.Empty;   // e.g. "ASP.NET Core Web API"
    public string? GitHubUrl { get; set; }
    public string? LiveUrl { get; set; }
    public int DisplayOrder { get; set; }
}
