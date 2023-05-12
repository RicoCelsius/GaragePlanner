using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.dto
{
    public class CarDto
    {
        public int? Id { get; set; }
        public string LicensePlate { get; set; }
        public string Color { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }

        public CarDto(int? id, string licensePlate, string color, string model, int year)
        {
            Id = id;
            LicensePlate = licensePlate;
            Color = color;
            Model = model;
            Year = year;
        }
    }
}
