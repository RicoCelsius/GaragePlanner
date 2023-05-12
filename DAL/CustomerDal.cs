using System.Data;
using System.Data.SqlClient;
using Core;
using Domain;
using Domain.dto;
using Domain.interfaces;
using MySqlConnector;




namespace DAL
{

    public class CustomerDal : ICustomerDal
    {
        public void InsertCustomer(Customer customer)
        {
            var query = "INSERT INTO customers (first_name, last_name, Address, Email, Password) " +
                        "VALUES (@first_name, @last_name, @Address, @Email, @Password)";
            MySqlParameter[] parameters =
            {
                new("@first_name", MySqlDbType.VarChar, 50) { Value = customer.FirstName },
                new("@last_name", MySqlDbType.VarChar, 50) { Value = customer.LastName },
                new("@Address", MySqlDbType.VarChar, 100) { Value = customer.Address },
                new("@Email", MySqlDbType.VarChar, 50) { Value = customer.Email },
                new("@Password", MySqlDbType.VarChar, 50) { Value = customer.Password }
            };

            var connection = new DbConnection();
            connection.ExecuteQuery(query, parameters);
        }

        public CustomerDto GetCustomerByEmail(string email)
        {
            var query = "SELECT * FROM customers WHERE Email = @Email";
            var parameters = new MySqlParameter[]
            {
                new MySqlParameter("@Email", email)
            };

            var connection = new DbConnection();
            var dataTable = connection.ExecuteQuery(query, parameters);

            if (dataTable.Rows.Count > 0)
            {
                var row = dataTable.Rows[0];
                var customerData = new CustomerDto(
                    row.Field<int>("id"),
                    row.Field<string>("first_name"),
                    row.Field<string>("last_name"),
                    row.Field<string>("Address"),
                    row.Field<string>("Email"),
                    row.Field<string>("Password"));

                return customerData;
            }

            throw new Exception("Customer not found");

        }

        public int GetCustomerIdByEmail(string email)
        {
            var query = "SELECT * FROM customers WHERE Email = @Email";
            var parameters = new MySqlParameter[]
            {
                new MySqlParameter("@Email", email)
            };

            var connection = new DbConnection();
            var dataTable = connection.ExecuteQuery(query, parameters);

            if (dataTable.Rows.Count > 0)
            {
                var row = dataTable.Rows[0];
                return row.Field<int>("id");
            }

            throw new Exception("Customer not found");

        }

        public List<CustomerDto> GetAllCustomers()
        {
            var query = "SELECT * FROM customers";
            var connection = new DbConnection();
            var parameters = new MySqlParameter[]
            {
                new MySqlParameter()
            };

            var dataTable = connection.ExecuteQuery(query,parameters);

            List<CustomerDto> customers = new List<CustomerDto>();
            foreach (DataRow row in dataTable.Rows)
            {
                CustomerDto customer = new(
                    row.Field<int>("id"),
                    row.Field<string>("first_name"),
                    row.Field<string>("last_name"),
                    row.Field<string>("Address"),
                    row.Field<string>("Email"),
                    row.Field<string>("Password"));
                customers.Add(customer);
            }

            return customers;
        }


    }
}