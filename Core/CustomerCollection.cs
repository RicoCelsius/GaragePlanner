using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Domain.dto;
using Domain.interfaces;

namespace Core
{
    public class CustomerCollection
    {
        public List<Customer> Customers { get; set; }
        private readonly ICustomerDal _iCustomerDal;

        public CustomerCollection(ICustomerDal iCustomerDal)
        {
            _iCustomerDal = iCustomerDal;
            Customers = new List<Customer>();
        }

        public void CreateCustomer(string firstName, string lastName, string address, string email, string password)
        {
            string encryptedPassword = PasswordEncryptor.EncryptPassword(password);
            Customer customer = new(firstName, lastName, address, email, encryptedPassword);
            _iCustomerDal.InsertCustomer(customer);
        }


        public Customer AuthenticateCustomer(string email, string inputPassword)
        {
            CustomerDto customerInfo = _iCustomerDal.GetCustomerByEmail(email);
            string hashedPassword = customerInfo.Password;
            string hashedInputPassword = PasswordEncryptor.EncryptPassword(inputPassword);

            if (hashedPassword == hashedInputPassword)
            {
                Customer customer = new(
                    customerInfo.FirstName,
                    customerInfo.LastName,
                    customerInfo.Address,
                    customerInfo.Email,
                    customerInfo.Password);
                return customer;
            }

            throw new Exception("Incorrect Password");
        }

        public CustomerDto GetCustomerByEmail(string email)
        {
            CustomerDto customerDto = _iCustomerDal.GetCustomerByEmail(email);
            return customerDto;
        }

        public List<string> GetCustomerEmails()
        {
            List<CustomerDto> customers = _iCustomerDal.GetAllCustomers();
            List<string> customerEmails = customers.Select(c => c.Email).ToList();
            return customerEmails;
        }
    }
}
