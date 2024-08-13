using PizzaHotApi.Data;
namespace PizzaHotApi.Models
{
    public class Animais
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public int Idade { get; set; }
        public string? Cor { get; set; }
        public string? Tipo { get; set; }
        public int Peso { get; set; }
    }
}
