﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DAL.models
{
    public class CustomerDalDto
    {
        public int? Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public CustomerDalDto(int? id, string firstName, string lastName, string address, string email, string password)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Address = address;
            Email = email;
            Password = password;
        }

    }
}
