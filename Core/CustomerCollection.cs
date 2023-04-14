using DAL;
using DAL.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace Core
{
    public class CustomerCollection
    {
        public List<Customer> Customers { get; set; }
        private readonly DALCustomer _dalCustomer;


        public CustomerCollection()
        {
            _dalCustomer = new DALCustomer();
            Customers = new List<Customer>();
        }


        public void CreateCustomer(string firstName, string lastName, string address, string email, string password)
        {
            string encryptedPassword = PasswordEncryptor.EncryptPassword(password);
            CustomerDalDto customerDto = new(null, firstName, lastName, address, email, encryptedPassword);
            _dalCustomer.InsertCustomer(customerDto);
        }




        public Customer AuthenticateCustomer(string email, string inputPassword)
        {
            CustomerDalDto customerInfo = _dalCustomer.GetCustomerByEmail(email);
            string hashedPassword = customerInfo.Password;
            string hashedInputPassword = PasswordEncryptor.EncryptPassword(inputPassword);

            if (hashedPassword == hashedInputPassword)
            {
                Customer customer = new(customerInfo.FirstName, customerInfo.LastName, customerInfo.Address, customerInfo.Email, customerInfo.Password);
                return customer;
            }

            throw new Exception("Incorrect password");


        }

    }
}
