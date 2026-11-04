namespace PortfolioApi.Models;

// One row of the Profile table: the "who am I" information shown on Home and About.
public class Profile
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public string? Headline { get; set; }
    public string Introduction { get; set; } = string.Empty;
    public string AboutMe { get; set; } = string.Empty;
    public string? Interests { get; set; }          // comma separated, e.g. "Full Stack Development, Data Analytics"
    public string? Email { get; set; }
    public string? GitHubUrl { get; set; }
    public string? LinkedInUrl { get; set; }
    public string? ResumeUrl { get; set; }
    public string? ProfileImageUrl { get; set; }
    public string? AvailabilityText { get; set; }   // e.g. "Open to work"
}
