using System.Data;
using MySqlConnector;
using Microsoft.Extensions.Configuration;


namespace DAL
{
    public class DbConnection
    {
        private readonly MySqlConnection _sqlConnection;
        private readonly string _connectionString;

        public DbConnection()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfigurationRoot configuration = builder.Build();
            _connectionString = configuration.GetConnectionString("DefaultConnection");

            _sqlConnection = new MySqlConnection(_connectionString);

        }

        private void Connect()
        {
            _sqlConnection.Open();
        }

        private void Disconnect()
        {
            _sqlConnection.Close();
        }

        public DataTable ExecuteQuery(string query, MySqlParameter[] parameters)
        {
            Connect();
            MySqlCommand command = new(query, _sqlConnection);
            if (parameters != null)
            {
                command.Parameters.AddRange(parameters);
            }

            MySqlDataAdapter adapter = new(command);
            DataTable dataTable = new();
            adapter.Fill(dataTable);
            Disconnect();
            return dataTable;
        }
    }
}