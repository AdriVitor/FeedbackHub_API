using FeedbackService_Domain.Entities;
using FeedbackService_Domain.Enums;
using FeedbackService_Infraestructure.Context;
using FeedbackService_Infraestructure.Repositories.Interfaces;

namespace FeedbackService_Infraestructure.Repositories
{
    public class FeedbackRepository : IFeedbackRepository
    {
        private readonly ContextDb _context;
        public FeedbackRepository(ContextDb context)
        {
            _context = context;
        }

        public async Task Insert(Feedback feedback)
        {
            await _context.Feedbacks.AddAsync(new Feedback(feedback.Id, feedback.IdProduct, (GradeEnum)feedback.Grade));
            await _context.SaveChangesAsync();
        }

        public IEnumerable<Feedback> GetAllByProduct(int idProduct)
        {
            return _context.Feedbacks.Where(f => f.IdProduct == idProduct);
        }
    }
}
