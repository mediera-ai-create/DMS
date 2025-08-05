using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class FeedbackController : ControllerBase
{
    private readonly IFeedbackService _feedbackService;

    public FeedbackController(IFeedbackService feedbackService)
    {
        _feedbackService = feedbackService;
    }

    [HttpPost]
    public async Task<IActionResult> AddFeedback([FromBody] FeedbackDto dto)
    {
        var result = await _feedbackService.AddFeedbackAsync(dto);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllFeedback()
    {
        var feedback = await _feedbackService.GetAllFeedbackAsync();
        return Ok(feedback);
    }
}
