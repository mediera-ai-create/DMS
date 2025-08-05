using DMS.Application.DTOs;
using DMS.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DMS.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DealerController : ControllerBase
    {
        private readonly IDealerService _dealerService;

        public DealerController(IDealerService dealerService)
        {
            _dealerService = dealerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            Ok(await _dealerService.GetAllDealersAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var dealer = await _dealerService.GetByDealerIdAsync(id);
            if (dealer == null) return NotFound();
            return Ok(dealer);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DealerDto dto)
        {
            var result = await _dealerService.AddDealerAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] DealerDto dto)
        {
            var updated = await _dealerService.UpdateDealerAsync(id, dto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _dealerService.DeleteDealerAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
