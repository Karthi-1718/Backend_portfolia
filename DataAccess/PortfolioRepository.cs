using Microsoft.Data.SqlClient;
using PortfolioApi.Interfaces;
using PortfolioApi.Models;

namespace PortfolioApi.DataAccess;

// Pure ADO.NET: SqlConnection -> SqlCommand -> SqlDataReader.
// (No Entity Framework, no Dapper.)
public class PortfolioRepository : IPortfolioRepository
{
    private readonly IDbConnectionFactory _factory;

    public PortfolioRepository(IDbConnectionFactory factory) => _factory = factory;

    public async Task<Profile?> GetProfileAsync(CancellationToken cancellationToken)
    {
        const string sql = @"
            SELECT TOP (1)
                   Id, FullName, Role, Headline, Introduction, AboutMe, Interests,
                   Email, GitHubUrl, LinkedInUrl, ResumeUrl, ProfileImageUrl, AvailabilityText
            FROM   Profile
            ORDER  BY Id;";

        // "await using" closes and disposes the connection automatically, even when an error happens.
        await using var connection = _factory.CreateConnection();
        await connection.OpenAsync(cancellationToken);

        await using var command = new SqlCommand(sql, connection);
        await using var reader = await command.ExecuteReaderAsync(cancellationToken);

        if (!await reader.ReadAsync(cancellationToken))
            return null;   // the table is empty

        return new Profile
        {
            Id = reader.GetInt("Id"),
            FullName = reader.GetRequiredString("FullName"),
            Role = reader.GetRequiredString("Role"),
            Headline = reader.GetNullableString("Headline"),
            Introduction = reader.GetRequiredString("Introduction"),
            AboutMe = reader.GetRequiredString("AboutMe"),
            Interests = reader.GetNullableString("Interests"),
            Email = reader.GetNullableString("Email"),
            GitHubUrl = reader.GetNullableString("GitHubUrl"),
            LinkedInUrl = reader.GetNullableString("LinkedInUrl"),
            ResumeUrl = reader.GetNullableString("ResumeUrl"),
            ProfileImageUrl = reader.GetNullableString("ProfileImageUrl"),
            AvailabilityText = reader.GetNullableString("AvailabilityText")
        };
    }

    public async Task<IReadOnlyList<Skill>> GetSkillsAsync(CancellationToken cancellationToken)
    {
        const string sql = "SELECT Id, Name, Category, DisplayOrder FROM Skills ORDER BY DisplayOrder, Id;";
        var skills = new List<Skill>();

        await using var connection = _factory.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        await using var command = new SqlCommand(sql, connection);
        await using var reader = await command.ExecuteReaderAsync(cancellationToken);

        while (await reader.ReadAsync(cancellationToken))
        {
            skills.Add(new Skill
            {
                Id = reader.GetInt("Id"),
                Name = reader.GetRequiredString("Name"),
                Category = reader.GetRequiredString("Category"),
                DisplayOrder = reader.GetInt("DisplayOrder")
            });
        }
        return skills;
    }

    public async Task<IReadOnlyList<Project>> GetProjectsAsync(CancellationToken cancellationToken)
    {
        const string sql = @"
            SELECT Id, Title, Description, TechStack, GitHubUrl, LiveUrl, DisplayOrder
            FROM   Projects
            ORDER  BY DisplayOrder, Id;";
        var projects = new List<Project>();

        await using var connection = _factory.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        await using var command = new SqlCommand(sql, connection);
        await using var reader = await command.ExecuteReaderAsync(cancellationToken);

        while (await reader.ReadAsync(cancellationToken))
        {
            projects.Add(new Project
            {
                Id = reader.GetInt("Id"),
                Title = reader.GetRequiredString("Title"),
                Description = reader.GetRequiredString("Description"),
                TechStack = reader.GetRequiredString("TechStack"),
                GitHubUrl = reader.GetNullableString("GitHubUrl"),
                LiveUrl = reader.GetNullableString("LiveUrl"),
                DisplayOrder = reader.GetInt("DisplayOrder")
            });
        }
        return projects;
    }

    public async Task<IReadOnlyList<Education>> GetEducationAsync(CancellationToken cancellationToken)
    {
        const string sql = @"
            SELECT Id, Degree, Institution, StartYear, EndYear, Description, DisplayOrder
            FROM   Education
            ORDER  BY DisplayOrder, Id;";
        var items = new List<Education>();

        await using var connection = _factory.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        await using var command = new SqlCommand(sql, connection);
        await using var reader = await command.ExecuteReaderAsync(cancellationToken);

        while (await reader.ReadAsync(cancellationToken))
        {
            items.Add(new Education
            {
                Id = reader.GetInt("Id"),
                Degree = reader.GetRequiredString("Degree"),
                Institution = reader.GetRequiredString("Institution"),
                StartYear = reader.GetInt("StartYear"),
                EndYear = reader.GetInt("EndYear"),
                Description = reader.GetNullableString("Description"),
                DisplayOrder = reader.GetInt("DisplayOrder")
            });
        }
        return items;
    }

    public async Task<IReadOnlyList<Certification>> GetCertificationsAsync(CancellationToken cancellationToken)
    {
        const string sql = @"
            SELECT Id, Title, Issuer, IssueYear, CredentialUrl, DisplayOrder
            FROM   Certifications
            ORDER  BY DisplayOrder, Id;";
        var items = new List<Certification>();

        await using var connection = _factory.CreateConnection();
        await connection.OpenAsync(cancellationToken);
        await using var command = new SqlCommand(sql, connection);
        await using var reader = await command.ExecuteReaderAsync(cancellationToken);

        while (await reader.ReadAsync(cancellationToken))
        {
            items.Add(new Certification
            {
                Id = reader.GetInt("Id"),
                Title = reader.GetRequiredString("Title"),
                Issuer = reader.GetRequiredString("Issuer"),
                IssueYear = reader.GetNullableInt("IssueYear"),
                CredentialUrl = reader.GetNullableString("CredentialUrl"),
                DisplayOrder = reader.GetInt("DisplayOrder")
            });
        }
        return items;
    }
}
