using Microsoft.EntityFrameworkCore;
using MotorBikeRental.Data;
using MotorBikeRental.Enum;
using MotorBikeRental.IRepositories;
using MotorBikeRental.IRepository;
using MotorBikeRental.Models;
using MotorBikeRental.ViewModels;

namespace MotorBikeRental.Repositories
{
    public class LocationRepository : ILocationRepository
    {
        private readonly DataContext _context = new DataContext();

        public bool Add(Location location)
        {
            try
            {
                _context.Locations.Add(location);
                _context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool Update(Location location)
        {
            try
            {
                var locationUpdate = _context.Locations.FirstOrDefault(x => x.Id == location.Id);
                locationUpdate.Startdate = location.Startdate;
                locationUpdate.Enddate = location.Enddate;
                locationUpdate.Valueforecast = location.Valueforecast;
                locationUpdate.Status = location.Status;
                locationUpdate.Idmotorcycle = location.Idmotorcycle;

                _context.Locations.Update(locationUpdate);
                _context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool Remove(int id)
        {
            try
            {
                var locations = _context.Locations.FirstOrDefault(x => x.Id == id);
                _context.Locations.Remove(locations);
                _context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public Location GetById(int id)
        {
            try
            {
                var location = _context.Locations.FirstOrDefault(x => x.Id == id);

                return location;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<Location> GetList()
        {
            try
            {
                var locations = _context.Locations.AsNoTracking().OrderBy(x => x.Id);

                return locations.ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public double RentMotorbike(DateTime startDate, DateTime enddate, int quantidadeDias, int idmotorcycle, int iddeliveryman)
        {
            try
            {
                var daily = 0;
                switch (quantidadeDias)
                {
                    case 7:
                        daily = (int)ERentalPrice.SeteDias;
                        break;
                    case 15:
                        daily = (int)ERentalPrice.QuinzeDias;
                        break;
                    case 30:
                        daily = (int)ERentalPrice.TrintaDias;
                        break;
                    default:
                        daily = 0;
                        break;
                }

                double princeFinal = (int)enddate.Subtract(startDate).TotalDays * daily;

                if(princeFinal > 0)
                {
                    var location = new Location(startDate, enddate, princeFinal, 1, idmotorcycle, iddeliveryman);
                    _context.Locations.Add(location);
                    _context.SaveChanges();
                    return princeFinal;
                }
                
                return 0;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public DevolutionMotorbikeView DevolutionMotorbike(int iddeliveryman)
        {
            try
            {
                DevolutionMotorbikeView devolutionMotorbikeView = new DevolutionMotorbikeView();

                var location = _context.Locations.FirstOrDefault(x => x.Iddeliveryman == iddeliveryman && x.Status == 1);
                location.Status = 0;
                devolutionMotorbikeView.IdMotorcycle = location.Idmotorcycle;

                int expectedDays = (int)location.Enddate.Subtract(location.Startdate).TotalDays;

                var daily = 0;
                switch (expectedDays)
                {
                    case 7:
                        daily = (int)ERentalPrice.SeteDias;
                        break;
                    case 15:
                        daily = (int)ERentalPrice.QuinzeDias;
                        break;
                    case 30:
                        daily = (int)ERentalPrice.TrintaDias;
                        break;
                    default:
                        daily = 0;
                        break;
                }

                double princeFinal = expectedDays * daily;

                int expiredDays = 0;
                int daysRemaining = 0;

                if (location.Enddate < DateTime.Now)
                {
                    expiredDays = (int)location.Enddate.Subtract(DateTime.Now).TotalDays * -1;
                    princeFinal += expiredDays * 50;
                }

                if (location.Enddate > DateTime.Now)
                {
                    int calculoPorcentagem = 0;
                    daysRemaining = (int)location.Enddate.Subtract(DateTime.Now).TotalDays;
                    switch (expectedDays)
                    {
                        case 7:
                            calculoPorcentagem = daysRemaining * (int)ERentalPrice.SeteDias;
                            princeFinal += 20 * calculoPorcentagem / 100;
                            break;
                        case 15:
                            calculoPorcentagem = daysRemaining * (int)ERentalPrice.QuinzeDias;
                            princeFinal += 40 * calculoPorcentagem / 100;
                            break;
                        case 30:
                            calculoPorcentagem = daysRemaining * (int)ERentalPrice.TrintaDias;
                            princeFinal += 60 * calculoPorcentagem / 100;
                            break;
                        default:
                            break;
                    }                
                }

                _context.Locations.Update(location);
                _context.SaveChanges();

                devolutionMotorbikeView.PrinceFinal = princeFinal;
                return devolutionMotorbikeView;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
