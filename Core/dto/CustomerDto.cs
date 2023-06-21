using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.dto
{
    public class CustomerDto
    {
        public int Id { get; set; }
        public string FirstName;
        public string LastName;
        public string Email;
        public string Address;
        public string Password;

        public CustomerDto(int id,string firstName, string lastName, string email, string address, string password)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Address = address;
            Password = password;
        }
    }
}
