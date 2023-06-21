using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Domain.dto;
using Domain.interfaces;

namespace GaragePlannerTests.mocks
{
    public class CustomerDalMock : ICustomerDal
    {
        private readonly List<CustomerDto> _customers;
        public bool HasInsertedCustomer;

        public CustomerDalMock(List<CustomerDto> customers){
            HasInsertedCustomer = false;
            this._customers = customers;
        }

        public List<CustomerDto> GetAllCustomers()
        {
            return _customers;
        }

        public CustomerDto GetCustomerByEmail(string email)
        {
            return _customers.FirstOrDefault(customer => customer.Email == email);
        }

        public void InsertCustomer(Customer customer)
        {
            HasInsertedCustomer = true;
        }

        public bool DoesCustomerExists(string email)
        {
            return _customers.Any(customer => customer.Email == email);
        }
    }
}
