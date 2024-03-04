using MotorBikeRental.Models;

namespace MotorBikeRental.IRepository
{
    public interface IMotorcycleRepository
    {
        bool Add(Motorcycle motorcycle);
        bool Update(Motorcycle category);
        bool Remove(int id);
        List<Motorcycle> GetList();
        Motorcycle GetById(int id);
        Motorcycle GetByPlate(string plate);
        Motorcycle GetMotorcycleAvailable();
    }
}
