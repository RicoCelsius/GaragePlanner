using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.interfaces
{
    public interface ICustomerDal
    {
        void InsertCustomer(Customer customer);
        Customer GetCustomerByEmail(string email);

        List<Customer> GetAllCustomers();
    }
}
