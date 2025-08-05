using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class LeadController : ControllerBase
{
    private readonly ILeadService _service;

    public LeadController(ILeadService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var lead = await _service.GetByIdAsync(id);
        return lead == null ? NotFound() : Ok(lead);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] LeadDto dto)
    {
        var created = await _service.AddAsync(dto);
        return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] LeadDto dto)
    {
        var updated = await _service.UpdateAsync(id, dto);
        return updated ? NoContent() : NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _service.DeleteAsync(id);
        return deleted ? NoContent() : NotFound();
    }
}
