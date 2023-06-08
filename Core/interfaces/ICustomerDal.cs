using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.dto;

namespace Domain.interfaces
{
    public interface ICustomerDal
    {
        void InsertCustomer(Customer customer);
        Task<CustomerDto> GetCustomerByEmailAsync(string email);

        Task<List<CustomerDto>> GetAllCustomersAsync();
    }
}
