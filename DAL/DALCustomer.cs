using System.Data;
using System.Data.SqlClient;
using Core;
using MySqlConnector;


namespace DAL;

public class DALCustomer : IDALCustomer
{
    public void InsertCustomer(string first_name, string last_name, string address, string email, string password)
    {
        var query = "INSERT INTO customers (first_name, last_name, address, email, password) " +
                    "VALUES (@first_name, @last_name, @address, @email, @password)";
        MySqlParameter[] parameters =
        {
            new("@first_name", MySqlDbType.VarChar, 50) { Value = first_name },
            new("@last_name", MySqlDbType.VarChar, 50) { Value = last_name },
            new("@address", MySqlDbType.VarChar, 100) { Value = address },
            new("@email", MySqlDbType.VarChar, 50) { Value = email },
            new("@password", MySqlDbType.VarChar, 50) { Value = password }
        };

        var connection = new DbConnection();
        connection.ExecuteQuery(query, parameters);
    }

    public Dictionary<string, object> GetCustomerByEmail(string email)
    {
        var query = "SELECT * FROM customers WHERE email = @email";
        var parameters = new MySqlParameter[]
        {
            new MySqlParameter("@email", email)
        };

        var connection = new DbConnection();
        var dataTable = connection.ExecuteQuery(query, parameters);

        if (dataTable.Rows.Count > 0)
        {
            var row = dataTable.Rows[0];
            var customerData = new Dictionary<string, object>
            {
                { "id", Convert.ToInt32(row["id"]) },
                { "first_name", row["first_name"].ToString() },
                { "last_name", row["last_name"].ToString() },
                { "address", row["address"].ToString() },
                { "email", email },
                { "password", row["password"].ToString() }
            };

            return customerData;
        }
        throw new Exception("Customer not found");

    }


}