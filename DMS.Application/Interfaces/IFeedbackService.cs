public interface IFeedbackService
{
    Task<Feedback> AddFeedbackAsync(FeedbackDto dto);
    Task<IEnumerable<Feedback>> GetAllFeedbackAsync();
}
