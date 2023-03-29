using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    internal class DALCustomer
    {


        public void insertCustomer(string firstName, string lastName, string address, string email, string password)
        {
            string query = "INSERT INTO Customers (FirstName, LastName, Address, Email, Password) " +
                           "VALUES (@FirstName, @LastName, @Address, @Email, @Password)";
            SqlParameter[] parameters = new SqlParameter[]
            {
            new SqlParameter("@FirstName", SqlDbType.VarChar, 50) { Value = firstName },
            new SqlParameter("@LastName", SqlDbType.VarChar, 50) { Value = lastName },
            new SqlParameter("@Address", SqlDbType.VarChar, 100) { Value = address },
            new SqlParameter("@Email", SqlDbType.VarChar, 50) { Value = email },
            new SqlParameter("@Password", SqlDbType.VarChar, 50) { Value = password }
            };

            DbConnection connection = new DbConnection();
            connection.ExecuteQuery(query, parameters);
        }

    }
}
