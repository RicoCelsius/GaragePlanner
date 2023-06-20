namespace Domain
{
    public class Car
    {
        public int? Id { get; set; }
        public string LicensePlate { get; set; }
        public Enums.Color Color { get; set; }
        public string Brand { get; set; }
        public int Year { get; set; }

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
