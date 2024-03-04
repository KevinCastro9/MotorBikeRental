using MotorBikeRental.Enum;

namespace MotorBikeRental.Models
{
    public class Deliveryman
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Cnpj { get; set; }
        public DateTime Dateofbirth { get; set; }
        public string Codcnh { get; set; }
        public string Typecnh { get; set; }
        public string Pathcnh { get; set; }
        public int Status { get; set; }

        public Deliveryman(string username, string password, string cnpj, DateTime dateofbirth, string codcnh, string typecnh, string pathcnh, int status)
        { 
            this.Username = username;
            this.Password = password;
            this.Cnpj = cnpj;
            this.Dateofbirth = dateofbirth;
            this.Codcnh = codcnh;
            this.Status = status;
            this.Pathcnh = pathcnh;
           
            switch (typecnh)
            {
                case "A":
                    this.Typecnh = ETypeCnh.A.ToString();
                    break;
                case "B":
                    this.Typecnh = ETypeCnh.A.ToString();
                    break;
                case "AB":
                    this.Typecnh = ETypeCnh.AB.ToString();
                    break;
                default:
                    this.Typecnh = null;
                    break;
            }
        }
    }
}
