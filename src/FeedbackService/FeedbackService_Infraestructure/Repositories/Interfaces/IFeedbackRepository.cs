using FeedbackService_Domain.Entities;

namespace FeedbackService_Infraestructure.Repositories.Interfaces
{
    public interface IFeedbackRepository
    {
        Task Insert(Feedback feedback);
        IEnumerable<Feedback> GetAllByProduct(int idProduct);
    }
}
