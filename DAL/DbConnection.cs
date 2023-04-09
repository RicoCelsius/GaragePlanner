using System;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using MySqlConnector;

namespace DAL
{
    public class DbConnection
    {
        private readonly string _connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=garageplanner";
        private readonly MySqlConnection _sqlConnection;

        private bool isConnected;

        public DbConnection()
        {
            _sqlConnection = new MySqlConnection(_connectionString);
            isConnected = false;
        }

        private void Connect()
        {
            try
            {
                if (isConnected == false)
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
                if (isConnected == true)
                {
                    _sqlConnection.Close();
                    isConnected = false;
                }
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
