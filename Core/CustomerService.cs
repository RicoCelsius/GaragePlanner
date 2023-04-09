using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;


namespace Core
{
    public class CustomerService : ICustomerService
    {
        private readonly IDALCustomer _dalCustomer;

        public CustomerService(IDALCustomer dalCustomer)
        {
            _dalCustomer = dalCustomer;
        }

        public void AddCustomer(string[] info)
        {
            _dalCustomer.InsertCustomer(info[0], info[1], info[2], info[3], info[4]);

        }

        public Customer AuthenticateCustomer(string inputEmail,string inputPassword)
        {
            Dictionary<string, object> customerInfo = _dalCustomer.GetCustomerByEmail(inputEmail);
            string password = customerInfo["password"].ToString();

            if (inputPassword == password)
            {
                string firstName = customerInfo["first_name"].ToString();
                string lastName = customerInfo["last_name"].ToString();
                string address = customerInfo["address"].ToString();
                string email = customerInfo["email"].ToString();
                 return new Customer(firstName, lastName, address, email, password);
            }

            throw new Exception("Incorrect password");




        }

    }
}

    