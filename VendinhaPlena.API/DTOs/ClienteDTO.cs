using System.ComponentModel.DataAnnotations;

namespace VendinhaPlena.API.DTOs
{
    public class ClienteCreateDTO
    {
        [Required(ErrorMessage = "O nome completo é obrigatório.")]
        [StringLength(100)]
        public string NomeCompleto { get; set; } = string.Empty;

        [Required(ErrorMessage = "O CPF é obrigatório.")]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "O CPF deve conter 11 dígitos numéricos.")]
        public string CPF { get; set; } = string.Empty;

        [Required(ErrorMessage = "A data de nascimento é obrigatória.")]
        public DateTime DataNascimento { get; set; }

        [EmailAddress(ErrorMessage = "E-mail inválido.")]
        [StringLength(100)]
        public string? Email { get; set; }
    }

    public class ClienteResponseDTO
    {
        public int Id { get; set; }
        public string NomeCompleto { get; set; } = string.Empty;
        public string CPF { get; set; } = string.Empty;
        public DateTime DataNascimento { get; set; }
        public int Idade { get; set; }
        public string? Email { get; set; }
        public decimal TotalDividas { get; set; }
    }
}
