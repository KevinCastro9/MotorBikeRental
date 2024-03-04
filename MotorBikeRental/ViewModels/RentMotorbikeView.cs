using MotorBikeRental.Models;

namespace MotorBikeRental.ViewModels
{
    public class RentMotorbikeView
    {
        public DateTime Dateinitial { get; set; }
        public DateTime Dateend { get; set; }
        public string Motorcyclemodel { get; set; }
        public string Motorcycleplate { get;}
        public double Simulatedprice { get; set; }

        public RentMotorbikeView(DateTime datainicial, DateTime dateend, string motorcyclemodel, string motorcycleplate, double simulatedprice)
        {
            Dateinitial = datainicial;
            Dateend = dateend;
            Motorcyclemodel = motorcyclemodel;
            Motorcycleplate = motorcycleplate;
            Simulatedprice = simulatedprice;   
        }
    }
}
