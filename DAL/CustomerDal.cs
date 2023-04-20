using System.Data;
using System.Data.SqlClient;
using Core;
using Domain;
using MySqlConnector;




namespace DAL
{

    public class CustomerDal : ICustomerDal
    {
        public void InsertCustomer(Customer customerDto)
        {
            var query = "INSERT INTO customers (first_name, last_name, address, email, password) " +
                        "VALUES (@first_name, @last_name, @address, @email, @password)";
            MySqlParameter[] parameters =
            {
                new("@first_name", MySqlDbType.VarChar, 50) { Value = customerDto.FirstName },
                new("@last_name", MySqlDbType.VarChar, 50) { Value = customerDto.LastName },
                new("@address", MySqlDbType.VarChar, 100) { Value = customerDto.Address },
                new("@email", MySqlDbType.VarChar, 50) { Value = customerDto.Email },
                new("@password", MySqlDbType.VarChar, 50) { Value = customerDto.Password }
            };

            var connection = new DbConnection();
            connection.ExecuteQuery(query, parameters);
        }

        public Customer GetCustomerByEmail(string email)
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
                var customerData = new Customer(
                    row.Field<string>("first_name"),
                    row.Field<string>("last_name"),
                    row.Field<string>("address"),
                    row.Field<string>("email"),
                    row.Field<string>("password"));

                return customerData;
            }

            throw new Exception("Customer not found");

        }


    }
}