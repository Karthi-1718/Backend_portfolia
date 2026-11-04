using Microsoft.Data.SqlClient;

namespace PortfolioApi.Interfaces;

// Creates a new SQL Server connection when asked.
public interface IDbConnectionFactory
{
    SqlConnection CreateConnection();
}
