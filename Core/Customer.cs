using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Customer
    {
        public string FirstName { get; }
        public string LastName { get; }
        public string Address { get; }
        public string Email { get; }
        public string Password { get; }


        public Customer(string firstName, string lastName, string address, string email, string password)
        {
            FirstName = firstName;
            LastName = lastName;
            Address = address;
            Email = email;
            Password = password;
        }


    }
}