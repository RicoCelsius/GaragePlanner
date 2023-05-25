using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Domain.dto;
using Domain.interfaces;
using Domain.utils;

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
            FillListWithCustomers();
        }

        public bool CreateCustomer(string firstName, string lastName, string address, string email, string password)
        {
            if (DoesEmailAlreadyExist(email))
            {
                return false;
            }

            string encryptedPassword = PasswordEncryptor.EncryptPassword(password);
            Customer customer = new(firstName, lastName, address, email, encryptedPassword);
            _iCustomerDal.InsertCustomer(customer);
            return true;
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

        public void FillListWithCustomers()
        {
            List<CustomerDto> customerDtos = _iCustomerDal.GetAllCustomers();
            foreach (CustomerDto customerDto in customerDtos)
            {
                Customer customer = DtoConverter.ConvertCustomerDtoToCustomer(customerDto);
                Customers.Add(customer);
            }
        }

        public bool DoesEmailAlreadyExist(string email)
        {
            bool customerExists = Customers.Any(c => c.Email == email);
            return customerExists;
        }


        public Customer GetCustomerByEmail(string email)
        {
            Customer customer = Customers.FirstOrDefault(c => c.Email == email);

            return customer;
        }



        public List<string> GetCustomerEmails()
        {
            FillListWithCustomers();
            List<string> customerEmails = Customers.Select(c => c.Email).ToList();
            return customerEmails;
        }


    }
}
