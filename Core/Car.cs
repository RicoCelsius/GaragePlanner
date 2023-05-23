using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Car
    {
        public string LicensePlate { get; set; }
        public string Color { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string? ImageUrl { get; set; } = "https://imageio.forbes.com/specials-images/imageserve/5d35eacaf1176b0008974b54/2020-Chevrolet-Corvette-Stingray/0x0.jpg?format=jpg&crop=4560,2565,x790,y784,safe&width=960";
        public Car(string licensePlate, string color, string model, int year)
        {
            this.LicensePlate = licensePlate;
            this.Color = color;
            this.Model = model;
            this.Year = year;
        }

    }



}
