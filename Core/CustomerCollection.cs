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
            string encryptedPassword = PasswordEncryptor.EncryptPassword(password);
            Customer customer = new(firstName, lastName, address, email, encryptedPassword);
            if (DoesEmailAlreadyExist(email))
            {
                return false;
            }

            try
            {
                _iCustomerDal.InsertCustomer(customer);
            }
            catch (Exception e)
            {
                throw new DalException("Could not create customer", e);
            }

            return true;
        }


        public bool AuthenticateCustomer(string email, string inputPassword)
        {
            CustomerDto customerInfo;
            try
            {
                customerInfo = _iCustomerDal.GetCustomerByEmail(email);
            }
            catch (Exception e)
            {
                throw new DalException("Could not authenticate customer", e);
            }

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


        private bool DoesEmailAlreadyExist(string email)
        {
            bool customerExists;
            try
            {
                customerExists = _iCustomerDal.DoesCustomerExists(email);
            }
            catch (Exception e)
            {
                throw new DalException("Could not check if customer exists", e);
            }

            return customerExists;
        }


        public List<string> GetCustomerEmails()
        {
            List<Customer> allCustomers;
            try
            {
                allCustomers = GetAllCustomers();
            }
            catch (Exception e)
            {
                throw new DalException("Could not get customer emails", e);
            }

            List<string> customerEmails = new();

            foreach (Customer customer in allCustomers)
            {
                customerEmails.Add(customer.Email);
            }

            return customerEmails;
        }


        public Customer GetCustomerByEmail(string email)
        {
            CustomerDto customerDto;
            try
            { 
                customerDto = _iCustomerDal.GetCustomerByEmail(email);
            }
            catch (Exception e)
            {
                throw new DalException("Could not get customer by email", e);
            }

            Customer customer = DtoConverter.ConvertCustomerDtoToCustomer(customerDto);
            return customer;
        }


        public List<Customer> GetAllCustomers()
        {
            List<CustomerDto> customersDto;
            try
            {
                customersDto = _iCustomerDal.GetAllCustomers();
            }
            catch(Exception e)
            {
                throw new DalException("Could not get all customers", e);
            }

            List<Customer> customers = new();
            foreach (CustomerDto customerDto in customersDto)
            {
                customers.Add(DtoConverter.ConvertCustomerDtoToCustomer(customerDto));
            }

            return customers;
        }

    }
}
