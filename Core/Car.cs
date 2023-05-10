using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Car
    {
        public int Id { get; set; }
        public string LicensePlate { get; set; }
        public string Color { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }

        public Car(string licensePlate, string color, string model, int year)
        {
            this.LicensePlate = licensePlate;
            this.Color = color;
            this.Model = model;
            this.Year = year;
        }
    }



}
