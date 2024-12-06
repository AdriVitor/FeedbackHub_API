using FeedbackService_Domain.Entities;

namespace FeedbackService_Application.Services.Interfaces
{
    public interface IFeedbackService
    {
        Task Insert(Feedback feedback);
        IEnumerable<Feedback> GetAllByProduct(int idProduct);
    }
}
