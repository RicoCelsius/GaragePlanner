using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.dto;

namespace Domain.interfaces
{
    public interface ICustomerDal
    {
        void InsertCustomer(Customer customer);
        CustomerDto GetCustomerByEmail(string email);

        List<Customer> GetAllCustomers();
    }
}
