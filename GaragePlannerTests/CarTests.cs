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
        public void AddCarButLicensePlateDoesNotAlreadyExists()
        {
            //Arrange
            var carDto = new CarDto(1, "1-2-3", Enums.Color.Black, "Mercedes", 1990);
            var carDtos = new List<CarDto>
            {
                carDto
            };
            var carDalMock = new CarDalMock(carDtos);
            var carCollection = new CarCollection(carDalMock);
            //Act
            bool AddResult = carCollection.TryCreateCar("test", "1-2-4", "Mercedes", Enums.Color.Black, 1990);
            //Assert
            Assert.True(AddResult);
            Assert.True(carDalMock.HasInsertedCar);

        }

        [Fact]
        public void AddCarButLicensePlateAlreadyExists()
        {
            //Arrange
            string licensePlate = "1-2-3";
            var carDto = new CarDto(1,licensePlate,Enums.Color.Black,"Mercedes",1990);
            var carDtos = new List<CarDto>
            {
                carDto
            };
            var carDalMock = new CarDalMock(carDtos);
            var carCollection = new CarCollection(carDalMock);
            //Act
            bool AddResult = carCollection.TryCreateCar("test", licensePlate, "Mercedes", Enums.Color.Black, 1990);
            //Assert
            Assert.False(AddResult);
            Assert.False(carDalMock.HasInsertedCar);

        }

    }
}