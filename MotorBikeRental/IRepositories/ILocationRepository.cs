using MotorBikeRental.Enum;
using MotorBikeRental.Models;
using MotorBikeRental.ViewModels;

namespace MotorBikeRental.IRepositories
{
    public interface ILocationRepository
    {
        bool Add(Location location);
        bool Update(Location location);
        bool Remove(int id);
        List<Location> GetList();
        Location GetById(int id);
        double RentMotorbike(DateTime startDate, DateTime enddate, int quantidadeDias, int idmotorcycle, int iddeliveryman);
        DevolutionMotorbikeView DevolutionMotorbike(int iddeliveryman);
    }
}
