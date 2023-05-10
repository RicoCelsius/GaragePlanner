﻿using System.Data;
using System.Data.SqlClient;
using Core;
using Domain;
using Domain.interfaces;
using MySqlConnector;




namespace DAL
{

    public class CustomerDal : ICustomerDal
    {
        public void InsertCustomer(Customer customer)
        {
            var query = "INSERT INTO customers (first_name, last_name, address, email, password) " +
                        "VALUES (@first_name, @last_name, @address, @email, @password)";
            MySqlParameter[] parameters =
            {
                new("@first_name", MySqlDbType.VarChar, 50) { Value = customer.FirstName },
                new("@last_name", MySqlDbType.VarChar, 50) { Value = customer.LastName },
                new("@address", MySqlDbType.VarChar, 100) { Value = customer.Address },
                new("@email", MySqlDbType.VarChar, 50) { Value = customer.Email },
                new("@password", MySqlDbType.VarChar, 50) { Value = customer.Password }
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

        public static int GetCustomerIdByEmail(string email)
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
                return row.Field<int>("id");
            }

            throw new Exception("Customer not found");

        }

        public List<Customer> GetAllCustomers()
        {
            var query = "SELECT * FROM customers";
            var connection = new DbConnection();
            var parameters = new MySqlParameter[]
            {
                new MySqlParameter()
            };

            var dataTable = connection.ExecuteQuery(query,parameters);

            List<Customer> customers = new List<Customer>();
            foreach (DataRow row in dataTable.Rows)
            {
                Customer customer = new(
                    row.Field<string>("first_name"),
                    row.Field<string>("last_name"),
                    row.Field<string>("address"),
                    row.Field<string>("email"),
                    row.Field<string>("password"));
                customers.Add(customer);
            }

            return customers;
        }


    }
}