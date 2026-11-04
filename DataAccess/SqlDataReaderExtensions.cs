using Microsoft.Data.SqlClient;

namespace PortfolioApi.DataAccess;

// Small helpers so the repository code stays easy to read.
// They look a column up BY NAME and turn database NULL into a C# null.
public static class SqlDataReaderExtensions
{
    public static string GetRequiredString(this SqlDataReader reader, string column)
        => reader.GetString(reader.GetOrdinal(column));

    public static string? GetNullableString(this SqlDataReader reader, string column)
    {
        var index = reader.GetOrdinal(column);
        return reader.IsDBNull(index) ? null : reader.GetString(index);
    }

    public static int GetInt(this SqlDataReader reader, string column)
        => reader.GetInt32(reader.GetOrdinal(column));

    public static int? GetNullableInt(this SqlDataReader reader, string column)
    {
        var index = reader.GetOrdinal(column);
        return reader.IsDBNull(index) ? null : reader.GetInt32(index);
    }
}
