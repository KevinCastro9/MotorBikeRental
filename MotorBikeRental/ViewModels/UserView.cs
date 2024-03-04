using MotorBikeRental.Enum;

namespace MotorBikeRental.ViewModels
{
    public class UserView
    {
        public int? ID { get; set; }
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public string Role { get; set; }
        public bool Status { get; set; }
    }
}
