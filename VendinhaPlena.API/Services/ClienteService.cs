using Microsoft.EntityFrameworkCore;
using VendinhaPlena.API.Data;
using VendinhaPlena.API.DTOs;
using VendinhaPlena.API.Models;

namespace VendinhaPlena.API.Services
{
    public class ClienteService : IClienteService
    {
        private readonly VendinhaDbContext _context;

        public ClienteService(VendinhaDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ClienteResponseDTO>> GetClientesAsync(string? nome, int page, int pageSize)
        {
            var query = _context.Clientes.Include(c => c.Dividas).AsQueryable();

            if (!string.IsNullOrEmpty(nome))
            {
                query = query.Where(c => c.NomeCompleto.Contains(nome));
            }

            return await query
                .OrderByDescending(c => c.Dividas.Where(d => d.Situacao == "Aberta").Sum(d => d.Valor))
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(c => new ClienteResponseDTO
                {
                    Id = c.Id,
                    NomeCompleto = c.NomeCompleto,
                    CPF = c.CPF,
                    DataNascimento = c.DataNascimento,
                    Idade = c.Idade,
                    Email = c.Email,
                    TotalDividas = c.Dividas.Where(d => d.Situacao == "Aberta").Sum(d => d.Valor)
                })
                .ToListAsync();
        }

        public async Task<ClienteResponseDTO?> GetClienteByIdAsync(int id)
        {
            var c = await _context.Clientes.Include(c => c.Dividas).FirstOrDefaultAsync(c => c.Id == id);
            if (c == null) return null;

            return new ClienteResponseDTO
            {
                Id = c.Id,
                NomeCompleto = c.NomeCompleto,
                CPF = c.CPF,
                DataNascimento = c.DataNascimento,
                Idade = c.Idade,
                Email = c.Email,
                TotalDividas = c.Dividas.Where(d => d.Situacao == "Aberta").Sum(d => d.Valor)
            };
        }

        public async Task<ClienteResponseDTO> CreateClienteAsync(ClienteCreateDTO dto)
        {
            var cliente = new Cliente
            {
                NomeCompleto = dto.NomeCompleto,
                CPF = dto.CPF,
                DataNascimento = dto.DataNascimento,
                Email = dto.Email
            };

            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();

            return new ClienteResponseDTO
            {
                Id = cliente.Id,
                NomeCompleto = cliente.NomeCompleto,
                CPF = cliente.CPF,
                DataNascimento = cliente.DataNascimento,
                Idade = cliente.Idade,
                Email = cliente.Email
            };
        }

        public async Task<bool> UpdateClienteAsync(int id, ClienteCreateDTO dto)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null) return false;

            cliente.NomeCompleto = dto.NomeCompleto;
            cliente.CPF = dto.CPF;
            cliente.DataNascimento = dto.DataNascimento;
            cliente.Email = dto.Email;

            _context.Entry(cliente).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteClienteAsync(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null) return false;

            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> CPFExistsAsync(string cpf)
        {
            return await _context.Clientes.AnyAsync(c => c.CPF == cpf);
        }
    }
}
