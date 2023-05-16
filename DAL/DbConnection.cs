using System.Data;
using MySqlConnector;
using Microsoft.Extensions.Configuration;


namespace DAL
{
    public class DbConnection
    {
        private readonly MySqlConnection _sqlConnection;
        private readonly string _connectionString;
        private bool isConnected;

        public DbConnection()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfigurationRoot configuration = builder.Build();
            _connectionString = configuration.GetConnectionString("DefaultConnection");

            _sqlConnection = new MySqlConnection(_connectionString);

            isConnected = false;
        }

        private void Connect()
        {
            if (isConnected)
            {
                throw new Exception("Cannot connect, because the application is already connected.");
            }

            try
            {
                _sqlConnection.Open();
                isConnected = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void Disconnect()
        {
            if (!isConnected)
            {
                throw new Exception("Cannot disconnect, because the application is not connected.");
            }

            try
            {

                _sqlConnection.Close();
                isConnected = false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
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