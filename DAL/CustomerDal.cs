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

        private readonly DbConnection _dbConnection;

        public CustomerDal(DbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public void InsertCustomer(Customer customer)
        {
            var query = "INSERT INTO customers (first_name, last_name, Address, Email, Password) " +
                        "VALUES (@first_name, @last_name, @Address, @Email, @Password)";
            MySqlParameter[] parameters =
            {
                new("@first_name", MySqlDbType.VarChar) { Value = customer.FirstName },
                new("@last_name", MySqlDbType.VarChar) { Value = customer.LastName },
                new("@Address", MySqlDbType.VarChar) { Value = customer.Address },
                new("@Email", MySqlDbType.VarChar) { Value = customer.Email },
                new("@Password", MySqlDbType.VarChar) { Value = customer.Password }
            };

            var connection = _dbConnection;
            connection.ExecuteNonQuery(query, parameters);
        }



        public bool DoesCustomerExists(string email)
        {
            var query = "SELECT * FROM customers WHERE Email = @Email";
            var parameters = new MySqlParameter[]
            {
                new MySqlParameter("@Email", email)
            };

            var connection = _dbConnection;
            var dataTable = connection.ExecuteQuery(query, parameters);

            return dataTable.Rows.Count > 0;
        }

        public CustomerDto GetCustomerByEmail(string email)
        {
            var query = "SELECT * FROM customers WHERE Email = @Email";
            var parameters = new MySqlParameter[]
            {
                new MySqlParameter("@Email", email)
            };

            var connection = _dbConnection;
            var dataTable = connection.ExecuteQuery(query, parameters);

            var row = dataTable.Rows[0];
            var customerData = new CustomerDto(
                row.Field<int>("id"),
                row.Field<string>("first_name"),
                row.Field<string>("last_name"),
                row.Field<string>("Email"),
                row.Field<string>("Address"),
                row.Field<string>("Password"));
            return customerData;

        }

        public List<CustomerDto> GetAllCustomers()
        {
            var query = "SELECT * FROM customers";
            var connection = _dbConnection;


            var dataTable = connection.ExecuteQuery(query, null);

            List<CustomerDto> customers = new List<CustomerDto>();
            foreach (DataRow row in dataTable.Rows)
            {
                CustomerDto customer = new(
                    row.Field<int>("id"),
                    row.Field<string>("first_name"),
                    row.Field<string>("last_name"),
                    row.Field<string>("Email"),
                    row.Field<string>("Address"),
                    row.Field<string>("Password"));
                customers.Add(customer);
            }

            return customers;
        }
    }





}
