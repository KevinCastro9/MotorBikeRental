using MotorBikeRental.Enum;

namespace MotorBikeRental.Models
{
    public class User
    {
        public int? ID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role {  get; set; }
        public bool Status { get; set; }

        public User(string username, string password, string role, bool status)
        {
            this.Username = username;
            this.Password = password;
            switch (role)
            {
                case "Admin":
                    this.Role = ERole.Admin.ToString();
                    break;
                default: 
                    this.Role = ERole.Standard.ToString(); 
                    break;
            }
            this.Status = status;
        }
    }
}
