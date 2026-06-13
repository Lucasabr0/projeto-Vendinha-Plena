using System.ComponentModel.DataAnnotations;

namespace VendinhaPlena.API.Models
{
    public class Cliente
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string NomeCompleto { get; set; } = string.Empty;

        [Required]
        [StringLength(11)]
        public string CPF { get; set; } = string.Empty;

        [Required]
        public DateTime DataNascimento { get; set; }

        [EmailAddress]
        [StringLength(100)]
        public string? Email { get; set; }

        public List<Divida> Dividas { get; set; } = new List<Divida>();

        // Propriedade calculada para Idade
        public int Idade => DateTime.Today.Year - DataNascimento.Year - (DateTime.Today < DataNascimento.AddYears(DateTime.Today.Year - DataNascimento.Year) ? 1 : 0);

        // Soma total das dívidas
        public decimal TotalDividas => Dividas.Sum(d => d.Valor);
    }
}
