using DAL;
using DAL.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace GaragePlannerTests.mocks
{
    internal class Mock : ICustomerDal
    {
        public Customer GetCustomerByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public void InsertCustomer(Customer customerDto)
        {
            throw new NotImplementedException();
        }
    }
}
