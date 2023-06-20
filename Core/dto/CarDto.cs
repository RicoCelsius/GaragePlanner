using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.dto
{
    public class CarDto
    {
        public int Id { get; set; }
        public string LicensePlate { get; set; }
        public Enums.Color Color { get; set; }
        public string Brand { get; set; }
        public int Year { get; set; }

        public CarDto(int id, string licensePlate, Enums.Color color, string brand, int year)
        {
            Id = id;
            LicensePlate = licensePlate;
            Color = color;
            Brand = brand;
            Year = year;
        }
    }
}
