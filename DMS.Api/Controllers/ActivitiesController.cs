using DMS.Application.DTOs;
using DMS.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace DMS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ActivitiesController : ControllerBase
    {
        private readonly IActivityService _service;

        public ActivitiesController(IActivityService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var activities = await _service.GetAllActivitiesAsync();
            return Ok(activities);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var activity = await _service.GetActivityByIdAsync(id);
            if (activity == null) return NotFound();
            return Ok(activity);
        }
        [HttpGet("{userid}")]
        public async Task<IActionResult> GetByUserId(string userid)
        {
            var activity = await _service.GetActivityByUserIdAsync(userid);
            if (activity == null) return NotFound();
            return Ok(activity);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ActivityDto dto)
        {
            var activity = await _service.AddActivityAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = activity.Id }, activity);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ActivityDto dto)
        {
            var updated = await _service.UpdateActivityAsync(id, dto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteActivityAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
