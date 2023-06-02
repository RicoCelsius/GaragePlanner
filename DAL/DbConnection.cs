using System.Data;
using System.Data.Common;
using Domain.utils;
using MySqlConnector;
using Microsoft.Extensions.Configuration;


namespace DAL
{
    public class DbConnection : IDbConnection
    {
        private readonly MySqlConnection _sqlConnection;

        public DbConnection(string connectionString)
        {
            _sqlConnection = new MySqlConnection(connectionString);
        }

        private void Connect()
        {
            if (_sqlConnection == null)
            {
                return;
            }

            if (_sqlConnection.State == ConnectionState.Closed)
            {
                _sqlConnection.Open();
            }

        }

        private void Disconnect()
        {
            if (_sqlConnection == null)
            {
                return;
            }

            if (_sqlConnection.State == ConnectionState.Open)
            {
                _sqlConnection.Close();
            }
        }

        public DataTable ExecuteQuery(string query, MySqlParameter[] parameters)
        {
            try
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
                return dataTable;
            }
            catch (DbException ex)
            {
                ex.Data.Add("Query", query);
                throw new CouldNotReadDataException("Error while executing query", ex);
            }
            finally
            {
                Disconnect();
            }
        }
    }
}