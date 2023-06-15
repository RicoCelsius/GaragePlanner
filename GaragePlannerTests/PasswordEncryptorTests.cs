using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.utils;

namespace GaragePlannerTests
{
    public class PasswordEncryptorTests
    {
        [Fact]
        public void EncryptPassword_ShouldReturnEncryptedPassword()
        {
            // Arrange
            string password = "password";
            
            // Act
            string actual = PasswordEncryptor.EncryptPassword(password);

            // Assert
            Assert.True(PasswordEncryptor.VerifyPassword(password,actual));
        }
    }
}
