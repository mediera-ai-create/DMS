using DMS.Application.DTOs;
using DMS.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DMS.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productservice)
        {
            _productService = productservice;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            Ok(await _productService.GetAllProductsAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _productService.GetByProductIdAsync(id);
            if (product == null) return NotFound();
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductDto dto)
        {
            var result = await _productService.AddProductAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ProductDto dto)
        {
            var updated = await _productService.UpdateProductAsync(id, dto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _productService.DeleteProductAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }

        [HttpGet("reorder-alerts")]
        public async Task<IActionResult> GetReorderAlerts()
        {
            var products = await _productService.GetProductsNeedingReorderAsync();
            return Ok(products);
        }

        [HttpGet("filter")]
        public async Task<IActionResult> FilterProducts([FromQuery] string? status, [FromQuery] int? dealerId, [FromQuery] DateTime? fromDate, [FromQuery] DateTime? toDate)
        {
            var products = await _productService.FilterProductsAsync(status, dealerId, fromDate, toDate);
            return Ok(products);
        }

        [HttpGet("aging-report")]
        public async Task<IActionResult> GetInventoryAgingReport()
        {
            var result = await _productService.GetInventoryAgingReportAsync();
            return Ok(result);
        }

    }
}
