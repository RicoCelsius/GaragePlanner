using System.Data;
using MySqlConnector;

namespace DAL;

public interface IDbConnection
{
    Task<DataTable> ExecuteQuery(string query, MySqlParameter[] parameters);
}