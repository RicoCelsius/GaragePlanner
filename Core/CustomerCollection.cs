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

        public Result CreateCustomer(string firstName, string lastName, string address, string email, string password)
        {
            if (DoesEmailAlreadyExist(email))
            {
                return new Result(false, "Email already exists");
            }

            string encryptedPassword = PasswordEncryptor.EncryptPassword(password);
            Customer customer = new(firstName, lastName, address, email, encryptedPassword);
            _iCustomerDal.InsertCustomer(customer);
            return new Result(true, "Customer created");
        }


        public Result AuthenticateCustomer(string email, string inputPassword)
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
                return new Result(true, "Customer authenticated");
            }

            return new Result(false, "Wrong password");
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
