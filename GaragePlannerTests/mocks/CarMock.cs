using System;
using System.Collections.Generic;
using Domain.dto;

namespace GaragePlannerTests.mocks
{
    internal class CarMock
    {
        public List<CarDto> GenerateCarDto(int amount)
        {
            List<CarDto> cars = new List<CarDto>();
            for (int i = 0; i < amount; i++)
            {
                cars.Add(new CarDto(i, "LicensePlate" + i, "Color" + i, "Model" + i, 2019));
            }

            return cars;
        }
    }
}