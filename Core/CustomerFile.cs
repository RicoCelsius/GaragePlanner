using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class CustomerFile
    {
        public List<Customer> Customers { get; set; }
        private readonly DALCustomer _dalCustomer;


        public CustomerFile()
        {
            _dalCustomer = new DALCustomer();
            Customers = new List<Customer>();
        }


        public void AddCustomer(Customer CustomerToBeAdded)
        {
            CustomerDto customerDto = new CustomerDto(null, CustomerToBeAdded.FirstName, CustomerToBeAdded.LastName, CustomerToBeAdded.Address, CustomerToBeAdded.Email, CustomerToBeAdded.Password);
            _dalCustomer.InsertCustomer(customerDto);
        }



        public Customer AuthenticateCustomer(Credentials inputData)
        {
            CustomerDto customerInfo = _dalCustomer.GetCustomerByEmail(inputData.Email);
            string password = customerInfo.Password;

            if (password == inputData.Password)
            {
                Customer customer = new Customer(customerInfo.FirstName, customerInfo.LastName, customerInfo.Address, customerInfo.Email, customerInfo.Password);
                return customer;
            }

            throw new Exception("Incorrect password");


        }

    }
}
