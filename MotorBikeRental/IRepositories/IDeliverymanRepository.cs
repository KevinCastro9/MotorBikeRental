using MotorBikeRental.Models;

namespace MotorBikeRental.IRepositories
{
    public interface IDeliverymanRepository
    {
        bool Add(Deliveryman deliveryman, IFormFile ImageCnh);
        bool Update(Deliveryman deliveryman, IFormFile? ImageCnh);
        bool Remove(int id);
        List<Deliveryman> GetList();
        Deliveryman GetById(int id);
        Deliveryman DeliveryManValidation(string username, string password);
    }
}
