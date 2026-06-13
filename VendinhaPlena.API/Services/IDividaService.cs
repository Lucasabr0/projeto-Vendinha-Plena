using VendinhaPlena.API.DTOs;

namespace VendinhaPlena.API.Services
{
    public interface IDividaService
    {
        Task<DividaResponseDTO?> GetDividaByIdAsync(int id);
        Task<IEnumerable<DividaResponseDTO>> GetDividasByClienteIdAsync(int clienteId);
        Task<DividaResponseDTO> CreateDividaAsync(DividaCreateDTO dividaDto);
        Task<bool> PagarDividaAsync(int id);
        Task<bool> ClienteTemDividaAbertaAsync(int clienteId);
    }
}
