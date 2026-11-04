using Microsoft.Data.SqlClient;
using PortfolioApi.Interfaces;

namespace PortfolioApi.DataAccess;

public class SqlConnectionFactory : IDbConnectionFactory
{
    private readonly string _connectionString;

    public SqlConnectionFactory(IConfiguration configuration)
    {
        // Reads ConnectionStrings:PortfolioDB from appsettings.json
        _connectionString = configuration.GetConnectionString("PortfolioDB")
            ?? throw new InvalidOperationException(
                "Connection string 'PortfolioDB' was not found. Check appsettings.json.");
    }

    // A SqlConnection is NOT opened here. The repository opens it, uses it and disposes it.
    public SqlConnection CreateConnection() => new SqlConnection(_connectionString);
}
