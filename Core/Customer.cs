using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Customer
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }


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