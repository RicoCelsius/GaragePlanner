using Core;
using Domain;
using Domain.dto;
using GaragePlannerTests.mocks;

namespace GaragePlannerTests
{
    public class CustomerTests
    {
        [Fact]
        public void AddCustomerButEmailAlreadyExists()
        {
            //Arrange
            var customerDto = new CustomerDto(1,"test", "test", "rico", "test", "test");
            var customerDtos = new List<CustomerDto>();
            customerDtos.Add(customerDto);
            var customerDalMock = new CustomerDalMock(customerDtos);
            var customerCollection = new CustomerCollection(customerDalMock);
            //Act
            bool hasAdded = customerCollection.CreateCustomer("test", "test", "test", "rico", "test");
            //Assert
            Assert.Single(customerCollection.Customers);
            Assert.False(hasAdded);


        }
    }
}