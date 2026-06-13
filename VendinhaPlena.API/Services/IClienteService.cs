using VendinhaPlena.API.DTOs;

namespace VendinhaPlena.API.Services
{
    public interface IClienteService
    {
        Task<IEnumerable<ClienteResponseDTO>> GetClientesAsync(string? nome, int page, int pageSize);
        Task<ClienteResponseDTO?> GetClienteByIdAsync(int id);
        Task<ClienteResponseDTO> CreateClienteAsync(ClienteCreateDTO clienteDto);
        Task<bool> UpdateClienteAsync(int id, ClienteCreateDTO clienteDto);
        Task<bool> DeleteClienteAsync(int id);
        Task<bool> CPFExistsAsync(string cpf);
    }
}
