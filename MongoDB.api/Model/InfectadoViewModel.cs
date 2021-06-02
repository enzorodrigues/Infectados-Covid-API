using System.ComponentModel.DataAnnotations;

namespace MongoDB.api.Model
{   
    /// <summary>
    /// Modelo de dados para inserção ou atualizaçao dos infectados pelo COVID-19.
    /// </summary>
    public class InfectadoViewModel
    {
        [Required(ErrorMessage = "CPF é obrigatório!")]
        public long cpf {get; set;}

        [Required(ErrorMessage = "Idade é obrigatório!")]
        public int Idade { get; set; }

        [Required(ErrorMessage = "Sexo é obrigatório!")]
        public string Sexo { get; set; }

        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}