namespace MotorBikeRental.Models
{
    public class Motorcycle
    {
        public int? ID { get; set; }
        public int Ano { get; set; }
        public string Modelo { get; set; }
        public string Placa { get; set; }
        public int Statulocacao { get; set; }
        public Motorcycle(int ano, string modelo, string placa, int statulocacao)
        {
            this.Ano = ano;
            this.Modelo = modelo;
            this.Placa = placa;
            Statulocacao = statulocacao;
        }

    }
}
