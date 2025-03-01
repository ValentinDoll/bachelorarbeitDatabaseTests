using System.Data.Common;
using System.Data.SqlClient;

namespace BachelorWebApp.Data.DBService; 

public interface ISqlConnectionFactory
{
    SqlConnection GetConnection();
}

public class SqlConnectionFactory : ISqlConnectionFactory
{
    private readonly string? _connectionString;

    public SqlConnectionFactory(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DBConnection");
    }

    public SqlConnection GetConnection()
    {
        var connection = new SqlConnection(_connectionString);
        connection.Open();

        using (var command = connection.CreateCommand())
        {
            command.CommandText = "SET XACT_ABORT ON;";
            command.ExecuteNonQuery();
        }

        return connection;
    }
}
