using System;
using System.Configuration;
using System.Data.SqlClient;

namespace DAL
{
    public class DbConnection : IDbConnection
    {
        private string _connectionString = "Server = 127.0.0.1; Database=garageplanner;Uid=root;Pwd=;";
        private SqlConnection _sqlConnection;
        private bool isConnected;

        public DbConnection()
        {
            _sqlConnection = new SqlConnection(_connectionString);
            isConnected = false;
        }

        public void Connect()
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

        public void Disconnect()
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

        public System.Data.DataTable ExecuteQuery(string query, SqlParameter[] parameters)
        {
            Connect();
            try
            {
                SqlCommand command = new SqlCommand(query, _sqlConnection);
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                System.Data.DataTable dataTable = new System.Data.DataTable();
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
