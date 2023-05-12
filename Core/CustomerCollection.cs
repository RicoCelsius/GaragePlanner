﻿using System;
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
            this._iCustomerDal = iCustomerDal;
            Customers = new List<Customer>();
        }


        public void CreateCustomer(string firstName, string lastName, string address, string email, string password)
        {
            /*if(DoesCustomerExist(Email))
                throw new Exception("Customer already exists");*/

            string encryptedPassword = PasswordEncryptor.EncryptPassword(password);
            Customer customer = new(firstName, lastName, address, email, encryptedPassword);
            _iCustomerDal.InsertCustomer(customer);
        }

        public bool DoesCustomerExist(string email)
        {
            try
            {
                CustomerDto customerInfo = _iCustomerDal.GetCustomerByEmail(email);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }




        public Customer AuthenticateCustomer(string email, string inputPassword)
        {
            CustomerDto customerInfo = _iCustomerDal.GetCustomerByEmail(email);
            string hashedPassword = customerInfo.Password;
            string hashedInputPassword = PasswordEncryptor.EncryptPassword(inputPassword);

            if (hashedPassword == hashedInputPassword)
            {
                Customer customer = new(customerInfo.FirstName, customerInfo.LastName, customerInfo.Address,
                    customerInfo.Email, customerInfo.Password);
                return customer;
            }

            throw new Exception("Incorrect Password");


        }

        public Customer GetCustomerByEmail(string email)
        {
            CustomerDto customerInfo = _iCustomerDal.GetCustomerByEmail(email);
            Customer customer = new(customerInfo.FirstName, customerInfo.LastName, customerInfo.Address,
                               customerInfo.Email, customerInfo.Password);
            return customer;
        }

       public int? GetCustomerIdByEmail(string emailAddress)
        {
            CustomerDto customerDto = _iCustomerDal.GetCustomerByEmail(emailAddress);
            int? id = customerDto.Id;

            return id;
        }

        public List<string> GetCustomerEmails()
        {
            List<CustomerDto> customers = _iCustomerDal.GetAllCustomers();
            List<string> customerEmails = customers.Select(c => c.Email).ToList();
            return customerEmails;
        }

    }
}
