using System.Data;
using System.Data.Common;
using Domain.utils;
using MySqlConnector;

namespace DAL
{
    public class DbConnection : IDbConnection
    {
        private readonly string _connectionString;

        public DbConnection(string connectionString)
        {
            _connectionString = connectionString;
        }

        public DataTable ExecuteQuery(string query, MySqlParameter[] parameters)
        {
            try
            {
                using (MySqlConnection sqlConnection = new MySqlConnection(_connectionString))
                {
                    sqlConnection.Open();

                    using (MySqlCommand command = new MySqlCommand(query, sqlConnection))
                    {
                        if (parameters != null)
                        {
                            command.Parameters.AddRange(parameters);
                        }

                        MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        return dataTable;
                    }
                }
            }
            catch (DbException ex)
            {
                ex.Data.Add("Query", query);
                throw new CouldNotReadDataException("Error while executing query", ex);
            }
        }
    }
}