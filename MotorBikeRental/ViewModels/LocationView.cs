using MotorBikeRental.Models;

namespace MotorBikeRental.ViewModels
{
    public class LocationView
    {
        public int Id { get; set; }
        public DateTime Startdate { get; set; }
        public DateTime Enddate { get; set; }
        public double Valueforecast { get; set; }
        public int Status { get; set; }
        public int Idmotorcycle { get; set; }
        public int Iddeliveryman { get; set; }
    }
}
