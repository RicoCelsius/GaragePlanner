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
        private readonly ICustomerDal _iCustomerDal;

        public CustomerCollection(ICustomerDal iCustomerDal)
        {
            _iCustomerDal = iCustomerDal;
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


        public bool AuthenticateCustomer(string email, string inputPassword)
        {
            CustomerDto customerInfo = _iCustomerDal.GetCustomerByEmail(email);
            string hashedInputPassword = PasswordEncryptor.EncryptPassword(inputPassword);

            if (PasswordEncryptor.VerifyPassword(inputPassword,hashedInputPassword))
            {
                Customer customer = new(
                    customerInfo.FirstName,
                    customerInfo.LastName,
                    customerInfo.Address,
                    customerInfo.Email,
                    customerInfo.Password);
                return true;
            }

            return false;
        }


        public bool DoesEmailAlreadyExist(string email)
        {
            bool customerExists = _iCustomerDal.DoesCustomerExists(email);
            return customerExists;
        }


        public List<string> GetCustomerEmails()
        {
            List<Customer> allCustomers = GetAllCustomers();
            List<string> customerEmails = new();

            foreach (Customer customer in allCustomers)
            {
                customerEmails.Add(customer.Email);
            }

            return customerEmails;
        }


        public Customer GetCustomerByEmail(string email)
        {
            CustomerDto customerDto = _iCustomerDal.GetCustomerByEmail(email);
            Customer customer = DtoConverter.ConvertCustomerDtoToCustomer(customerDto);
            return customer;
        }


        public List<Customer> GetAllCustomers()
        {
            List<CustomerDto> customersDto = _iCustomerDal.GetAllCustomers();
            List<Customer> customers = new();
            foreach (CustomerDto customerDto in customersDto)
            {
                customers.Add(DtoConverter.ConvertCustomerDtoToCustomer(customerDto));
            }

            return customers;
        }

    }
}
