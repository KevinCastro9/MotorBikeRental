using Microsoft.EntityFrameworkCore;
using MotorBikeRental.Data;
using MotorBikeRental.Enum;
using MotorBikeRental.IRepository;
using MotorBikeRental.Models;

namespace MotorBikeRental.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context = new DataContext();

        public bool Add(User user)
        {
            try
            {
                _context.Users.Add(user);
                _context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Update(User user)
        {
            try
            {
                var userUpdate = _context.Users.FirstOrDefault(x => x.ID == user.ID);

                if (user.Username != null && user.Username != "")
                    userUpdate.Username = user.Username;
                if (user.Password != null && user.Password != "")
                    userUpdate.Password = user.Password;
                if (user.Role != null && user.Role != "")
                    userUpdate.Role = user.Role;
                if (user.Status != null)
                    userUpdate.Status = user.Status;

                _context.Users.Update(userUpdate);
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
                var user = _context.Users.FirstOrDefault(x => x.ID == id);
                _context.Users.Remove(user);
                _context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<User> GetList()
        {
            try
            {
                var users = _context.Users.AsNoTracking().OrderBy(x => x.ID);
                return users.ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public User GetById(int id)
        {
            try
            {
                var users = _context.Users.FirstOrDefault(x => x.ID == id);
                return users;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public User GetUserAuthentication(string userName, string password)
        {
            try
            {
                var users = _context.Users.AsNoTracking().FirstOrDefault(x => x.Username == userName && x.Password == password);
                return users;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
