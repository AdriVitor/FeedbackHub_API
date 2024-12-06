using FeedbackService_Application.Services.Interfaces;
using FeedbackService_Domain.Entities;
using FeedbackService_Infraestructure.Repositories.Interfaces;

namespace FeedbackService_Application.Services
{
    public class FeedbackService : IFeedbackService
    {
        private readonly IFeedbackRepository _feedbackRepository;
        public FeedbackService(IFeedbackRepository feedbackRepository)
        {
            _feedbackRepository = feedbackRepository;
        }

        public async Task Insert(Feedback feedback)
        {
            await _feedbackRepository.Insert(feedback);
        }

        public IEnumerable<Feedback> GetAllByProduct(int idProduct)
        {
            var feedbacks = _feedbackRepository.GetAllByProduct(idProduct);

            return feedbacks ?? throw new Exception("Nenhum feedback encontrado para esse produto");
        }
    }
}
