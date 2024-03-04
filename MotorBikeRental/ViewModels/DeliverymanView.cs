namespace MotorBikeRental.ViewModels
{
    public class DeliverymanView
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Cnpj { get; set; }
        public DateTime Dateofbirth { get; set; }
        public string Codcnh { get; set; }
        public string Typecnh { get; set; }
        public int Status { get; set; }
        public IFormFile? ImageCnh { get; set; }
    }
}
