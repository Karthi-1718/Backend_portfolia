namespace PortfolioApi.Models;

public class Certification
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Issuer { get; set; } = string.Empty;
    public int? IssueYear { get; set; }
    public string? CredentialUrl { get; set; }
    public int DisplayOrder { get; set; }
}
