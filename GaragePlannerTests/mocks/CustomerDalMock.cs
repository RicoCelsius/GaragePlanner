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

        public CustomerDalMock(List<CustomerDto> customers){
            this._customers = customers;
        }

        public List<CustomerDto> GetAllCustomers()
        {
            return _customers;
        }

        public CustomerDto GetCustomerByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public void InsertCustomer(Customer customer)
        {
            
        }
    }
}
