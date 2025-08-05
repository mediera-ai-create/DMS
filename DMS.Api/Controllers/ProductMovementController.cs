using DMS.Application.DTOs;
using DMS.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DMS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductMovementController : ControllerBase
    {
        private readonly IProductMovementService _service;

        public ProductMovementController(IProductMovementService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] ProductMovementDto dto)
        {
            var result = await _service.AddMovementAsync(dto);
            return Ok(result);
        }

        [HttpGet("{productId}")]
        public async Task<IActionResult> GetByProduct(int productId)
        {
            var result = await _service.GetMovementsByProductAsync(productId);
            return Ok(result);
        }
    }
}
