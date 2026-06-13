using Microsoft.AspNetCore.Mvc;
using VendinhaPlena.API.DTOs;
using VendinhaPlena.API.Services;
using VendinhaPlena.API.Utils;

namespace VendinhaPlena.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteService _clienteService;

        public ClientesController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClienteResponseDTO>>> GetClientes(string? nome = null, int page = 1, int pageSize = 10)
        {
            var clientes = await _clienteService.GetClientesAsync(nome, page, pageSize);
            return Ok(clientes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ClienteResponseDTO>> GetCliente(int id)
        {
            var cliente = await _clienteService.GetClienteByIdAsync(id);
            if (cliente == null) return NotFound();
            return Ok(cliente);
        }

        [HttpPost]
        public async Task<ActionResult<ClienteResponseDTO>> PostCliente(ClienteCreateDTO dto)
        {
            if (!CpfValidator.IsValid(dto.CPF))
                return BadRequest("CPF inválido.");

            if (await _clienteService.CPFExistsAsync(dto.CPF))
                return BadRequest("CPF já cadastrado.");

            var cliente = await _clienteService.CreateClienteAsync(dto);
            return CreatedAtAction(nameof(GetCliente), new { id = cliente.Id }, cliente);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCliente(int id, ClienteCreateDTO dto)
        {
            if (!CpfValidator.IsValid(dto.CPF))
                return BadRequest("CPF inválido.");

            var success = await _clienteService.UpdateClienteAsync(id, dto);
            if (!success) return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            var success = await _clienteService.DeleteClienteAsync(id);
            if (!success) return NotFound();

            return NoContent();
        }
    }
}
