using Core;
using Domain;
using Domain.dto;
using Domain.utils;
using GaragePlannerTests.mocks;

namespace GaragePlannerTests
{
    public class CarTests
    {
        [Fact]
        public void AddCarButLicensePlateAlreadyExists()
        {
            //Arrange
            var carDto = new CarDto(1,"1-2-3",Enums.Color.Black,"Mercedes",1990);
            var carDtos = new List<CarDto>();
            carDtos.Add(carDto);
            var carDalMock = new CarDalMock(carDtos);
            var carCollection = new CarCollection(carDalMock);
            //Act
            Result AddResult = carCollection.CreateCar("test", new Car("test", Enums.Color.Black, "test", 1990));
            //Assert
            Assert.False(AddResult.Success);

        }

    }
}