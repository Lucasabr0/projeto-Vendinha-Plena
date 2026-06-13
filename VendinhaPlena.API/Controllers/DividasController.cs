using Microsoft.AspNetCore.Mvc;
using VendinhaPlena.API.DTOs;
using VendinhaPlena.API.Services;

namespace VendinhaPlena.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DividasController : ControllerBase
    {
        private readonly IDividaService _dividaService;

        public DividasController(IDividaService dividaService)
        {
            _dividaService = dividaService;
        }

        [HttpPost]
        public async Task<ActionResult<DividaResponseDTO>> PostDivida(DividaCreateDTO dto)
        {
            if (await _dividaService.ClienteTemDividaAbertaAsync(dto.ClienteId))
                return BadRequest("O cliente já possui uma dívida em aberto.");

            var divida = await _dividaService.CreateDividaAsync(dto);
            return CreatedAtAction(nameof(GetDivida), new { id = divida.Id }, divida);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DividaResponseDTO>> GetDivida(int id)
        {
            var divida = await _dividaService.GetDividaByIdAsync(id);
            if (divida == null) return NotFound();
            return Ok(divida);
        }

        [HttpGet("cliente/{clienteId}")]
        public async Task<ActionResult<IEnumerable<DividaResponseDTO>>> GetDividasPorCliente(int clienteId)
        {
            var dividas = await _dividaService.GetDividasByClienteIdAsync(clienteId);
            return Ok(dividas);
        }

        [HttpPatch("{id}/pagar")]
        public async Task<IActionResult> PagarDivida(int id)
        {
            var success = await _dividaService.PagarDividaAsync(id);
            if (!success) return BadRequest("Dívida não encontrada ou já paga.");

            return NoContent();
        }
    }
}
