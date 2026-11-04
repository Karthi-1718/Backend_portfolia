using System.Data;
using Microsoft.Data.SqlClient;
using PortfolioApi.Interfaces;
using PortfolioApi.Models;

namespace PortfolioApi.DataAccess;

public class ContactRepository : IContactRepository
{
    private readonly IDbConnectionFactory _factory;

    public ContactRepository(IDbConnectionFactory factory) => _factory = factory;

    public async Task<int> AddAsync(ContactMessage message, CancellationToken cancellationToken)
    {
        // @Name, @Email ... are PARAMETERS. The values are sent separately from the SQL text,
        // so a visitor can never inject SQL through the contact form.
        // CreatedAt is filled in by the database (DEFAULT SYSUTCDATETIME()).
        const string sql = @"
            INSERT INTO ContactMessages (Name, Email, Subject, Message)
            OUTPUT INSERTED.Id
            VALUES (@Name, @Email, @Subject, @Message);";

        await using var connection = _factory.CreateConnection();
        await connection.OpenAsync(cancellationToken);

        await using var command = new SqlCommand(sql, connection);
        command.Parameters.Add("@Name", SqlDbType.NVarChar, 100).Value = message.Name;
        command.Parameters.Add("@Email", SqlDbType.NVarChar, 150).Value = message.Email;
        command.Parameters.Add("@Subject", SqlDbType.NVarChar, 150).Value = message.Subject;
        command.Parameters.Add("@Message", SqlDbType.NVarChar, 2000).Value = message.Message;

        // OUTPUT INSERTED.Id returns the new row's Id, so ExecuteScalar gives it back to us.
        var newId = await command.ExecuteScalarAsync(cancellationToken);
        return Convert.ToInt32(newId);
    }
}
