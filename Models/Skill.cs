namespace PortfolioApi.Models;

public class Skill
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;   // Backend, Database, Frontend ...
    public int DisplayOrder { get; set; }
}
