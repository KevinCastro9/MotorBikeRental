using MotorBikeRental.Models;

namespace MotorBikeRental.IRepository
{
    public interface IUserRepository
    {
        bool Add(User user);
        bool Update(User user);
        bool Remove(int id);
        List<User> GetList();
        User GetById(int id);
        User GetUserAuthentication(string userName, string password);
    }
}
