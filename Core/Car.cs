namespace Domain
{
    public class Car
    {
        public int? Id { get; }
        public string LicensePlate { get; }
        public Enums.Color Color { get;}
        public string Brand { get;}
        public int Year { get; }

        public Car(string licensePlate, Enums.Color color, string brand, int year)
        {
            this.LicensePlate = licensePlate;
            this.Color = color;
            this.Brand = brand;
            this.Year = year;
            }

        public Car(int? id, string licensePlate, Enums.Color color, string brand, int year)
            : this(licensePlate, color, brand, year)
        {
            this.Id = id;
        }
    }
}
