using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Domain.dto;
using Domain.interfaces;

namespace GaragePlannerTests.mocks
{
    internal class CustomerMock
    {
        public List<CustomerDto> GenerateCustomers(int amount)
        {
            List<CustomerDto> customers = new List<CustomerDto>();
            for (int i = 0; i < amount; i++)
            {
                customers.Add(new CustomerDto(i, "FirstName" + i, "LastName" + i, "Address" + i, "Email" + i, "Password" + i));
            }

            return customers;
        }
    }
}