using DMS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

public class FeedbackService : IFeedbackService
{
    private readonly DmsDbContext _context;

    public FeedbackService(DmsDbContext context)
    {
        _context = context;
    }

    public async Task<Feedback> AddFeedbackAsync(FeedbackDto dto)
    {
        var feedback = new Feedback
        {
            DealerId = dto.DealerId,
            CustomerId = dto.CustomerId,
            Rating = dto.Rating,
            Comments = dto.Comments,
            SubmittedAt = dto.SubmittedAt
        };

        _context.Feedbacks.Add(feedback);
        await _context.SaveChangesAsync();
        return feedback;
    }

    public async Task<IEnumerable<Feedback>> GetAllFeedbackAsync()
    {
        return await _context.Feedbacks
            .Include(f => f.Dealer)
            .Include(f => f.Customer)
            .ToListAsync();
    }
}
