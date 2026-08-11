namespace Maps_Dappers.Services
{

    public class City
    {
        public long Id { get; set; }
        public string Name { get; set; } = default!;
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int? SnapChartValues { get; set; }
    }

}
