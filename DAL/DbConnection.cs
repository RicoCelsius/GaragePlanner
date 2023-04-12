using System.Data;
using MySqlConnector;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;



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
            try
            {
                if (!isConnected)
                {
                    _sqlConnection.Open();
                    isConnected = true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to connect to the database.", ex);
            }
        }

        private void Disconnect()
        {
            try
            {
                if (!isConnected)
                {
                    return;
                }
                _sqlConnection.Close();
                isConnected = false;
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to disconnect from the database.", ex);
            }
        }



        public DataTable ExecuteQuery(string query, MySqlParameter[] parameters)
        {
            Connect();
            try
            {
                MySqlCommand command = new (query, _sqlConnection);
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }
                MySqlDataAdapter adapter = new (command);
                DataTable dataTable = new();
                adapter.Fill(dataTable);
                return dataTable;
            }
            catch (Exception ex)
            {
                throw new Exception("Error executing query.", ex);
            }
            finally
            {
                Disconnect();
            }
        }




    }
}
