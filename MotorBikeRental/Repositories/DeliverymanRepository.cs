using Microsoft.EntityFrameworkCore;
using MotorBikeRental.Data;
using MotorBikeRental.Enum;
using MotorBikeRental.IRepositories;
using MotorBikeRental.Models;
using MotorBikeRental.ViewModels;

namespace MotorBikeRental.Repositories
{
    public class DeliverymanRepository : IDeliverymanRepository
    {
        private readonly DataContext _context = new DataContext();
        public bool Add(Deliveryman deliveryman, IFormFile ImageCnh)
        {
            try
            {
                _context.Deliverymans.Add(deliveryman);
                _context.SaveChanges();

                using Stream fileStream = new FileStream(deliveryman.Pathcnh, FileMode.Create);
                ImageCnh.CopyTo(fileStream);

                return true;
            }
            catch (Exception ex)
            { 
                return false;
            }
        }
        public bool Update(Deliveryman deliveryman, IFormFile? ImageCnh)
        {
            try
            {
                var deliverymanUpdate = _context.Deliverymans.FirstOrDefault(x => x.Id == deliveryman.Id);

                deliverymanUpdate.Dateofbirth = deliveryman.Dateofbirth;

                if (deliveryman.Username != null && deliveryman.Username != "")
                    deliverymanUpdate.Username = deliveryman.Username;
                
                if (deliveryman.Password != null && deliveryman.Password != "")
                    deliverymanUpdate.Password = deliveryman.Password;

                if (deliveryman.Cnpj != null && deliveryman.Cnpj != "")
                    deliverymanUpdate.Cnpj = deliveryman.Cnpj;

                if (deliveryman.Codcnh != null && deliveryman.Codcnh != "")
                    deliverymanUpdate.Codcnh = deliveryman.Codcnh;

                if (deliveryman.Typecnh != null && deliveryman.Typecnh != "")
                    deliverymanUpdate.Typecnh = deliveryman.Typecnh;

                if (deliveryman.Status != null && deliveryman.Status <= 1)
                    deliverymanUpdate.Status = deliveryman.Status;

                if (deliveryman.Pathcnh != null && deliveryman.Pathcnh != "")
                    deliverymanUpdate.Pathcnh = deliveryman.Pathcnh;

                _context.Deliverymans.Update(deliverymanUpdate);
                _context.SaveChanges();

                if (ImageCnh != null)
                {
                    using Stream fileStream = new FileStream(deliveryman.Pathcnh, FileMode.Create);
                    ImageCnh.CopyTo(fileStream);
                }

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
                var deliveryman = _context.Deliverymans.FirstOrDefault(x => x.Id == id);
                _context.Deliverymans.Remove(deliveryman);
                _context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public Deliveryman GetById(int id)
        {
            try
            {
                var deliveryman = _context.Deliverymans.FirstOrDefault(x => x.Id == id);

                return deliveryman;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<Deliveryman> GetList()
        {
            try
            {
                var deliverymans = _context.Deliverymans.AsNoTracking().OrderBy(x => x.Id);

                return deliverymans.ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Deliveryman DeliveryManValidation(string username, string password)
        {
            try
            {
                var deliveryman = _context.Deliverymans.AsNoTracking().FirstOrDefault(x => x.Username == username && x.Password == password && (x.Typecnh == ETypeCnh.A.ToString() || x.Typecnh == ETypeCnh.AB.ToString()));

                return deliveryman;
            }
            catch
            {
                return null;
            }
        }
    }
}
