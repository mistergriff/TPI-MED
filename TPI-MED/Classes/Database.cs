using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;
using TPI_MED;

public class Database
{
    private static string connectionString = Program.Configuration.GetConnectionString("Default");

    public static MySqlConnection GetConnection()
    {
        return new MySqlConnection(connectionString);
    }
}