namespace MotorBikeRental.Models
{
    public class Location
    {
        public int Id { get; set; }
        public DateTime Startdate { get; set; }
        public DateTime Enddate { get; set; }
        public double Valueforecast { get; set; }
        public int Status { get; set; } 
        public int Idmotorcycle { get; set; }
        public int Iddeliveryman { get; set; }

        public Location(DateTime startdate, DateTime enddate, double valueforecast, int status, int idmotorcycle, int iddeliveryman)
        {
            this.Startdate = startdate;
            this.Enddate = enddate;
            this.Valueforecast = valueforecast;
            this.Status = status;
            this.Idmotorcycle = idmotorcycle;
            Iddeliveryman = iddeliveryman;
        }


    }
}
