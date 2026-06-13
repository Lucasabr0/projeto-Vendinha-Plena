using System.ComponentModel.DataAnnotations;

namespace VendinhaPlena.API.DTOs
{
    public class DividaCreateDTO
    {
        [Required(ErrorMessage = "O ID do cliente é obrigatório.")]
        public int ClienteId { get; set; }

        [Required(ErrorMessage = "O valor da dívida é obrigatório.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O valor deve ser maior que zero.")]
        public decimal Valor { get; set; }
    }

    public class DividaResponseDTO
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public decimal Valor { get; set; }
        public string Situacao { get; set; } = string.Empty;
        public DateTime DataCriacao { get; set; }
        public DateTime? DataPagamento { get; set; }
    }
}
