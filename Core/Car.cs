namespace Domain
{
    public class Car
    {
        public int Id { get; set; }
        public string LicensePlate { get; set; }
        public Enums.Color Color { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string ImageUrl { get; set; }

        public Car(string licensePlate, Enums.Color color, string model, int year, string imageUrl = "")
        {
            this.LicensePlate = licensePlate;
            this.Color = color;
            this.Model = model;
            this.Year = year;
            this.ImageUrl = imageUrl;
            }

        public Car(int id, string licensePlate, Enums.Color color, string model, int year, string imageUrl = "")
            : this(licensePlate, color, model, year, imageUrl)
        {
            this.Id = id;
        }
    }
}
