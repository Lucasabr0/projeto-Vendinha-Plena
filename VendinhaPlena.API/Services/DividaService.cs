using Microsoft.EntityFrameworkCore;
using VendinhaPlena.API.Data;
using VendinhaPlena.API.DTOs;
using VendinhaPlena.API.Models;

namespace VendinhaPlena.API.Services
{
    public class DividaService : IDividaService
    {
        private readonly VendinhaDbContext _context;

        public DividaService(VendinhaDbContext context)
        {
            _context = context;
        }

        public async Task<DividaResponseDTO?> GetDividaByIdAsync(int id)
        {
            var d = await _context.Dividas.FindAsync(id);
            if (d == null) return null;

            return MapToDTO(d);
        }

        public async Task<IEnumerable<DividaResponseDTO>> GetDividasByClienteIdAsync(int clienteId)
        {
            return await _context.Dividas
                .Where(d => d.ClienteId == clienteId)
                .Select(d => MapToDTO(d))
                .ToListAsync();
        }

        public async Task<DividaResponseDTO> CreateDividaAsync(DividaCreateDTO dto)
        {
            var divida = new Divida
            {
                ClienteId = dto.ClienteId,
                Valor = dto.Valor,
                Situacao = "Aberta",
                DataCriacao = DateTime.Now
            };

            _context.Dividas.Add(divida);
            await _context.SaveChangesAsync();

            return MapToDTO(divida);
        }

        public async Task<bool> PagarDividaAsync(int id)
        {
            var divida = await _context.Dividas.FindAsync(id);
            if (divida == null || divida.Situacao == "Paga") return false;

            divida.Situacao = "Paga";
            divida.DataPagamento = DateTime.Now;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ClienteTemDividaAbertaAsync(int clienteId)
        {
            return await _context.Dividas.AnyAsync(d => d.ClienteId == clienteId && d.Situacao == "Aberta");
        }

        private static DividaResponseDTO MapToDTO(Divida d)
        {
            return new DividaResponseDTO
            {
                Id = d.Id,
                ClienteId = d.ClienteId,
                Valor = d.Valor,
                Situacao = d.Situacao,
                DataCriacao = d.DataCriacao,
                DataPagamento = d.DataPagamento
            };
        }
    }
}
