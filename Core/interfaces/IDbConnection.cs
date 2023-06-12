using System.Data;
using MySqlConnector;

namespace DAL;

public interface IDbConnection 
{
    DataTable ExecuteQuery(string query, MySqlParameter[] parameters);
}