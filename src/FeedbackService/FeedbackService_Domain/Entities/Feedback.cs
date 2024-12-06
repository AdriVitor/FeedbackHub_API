using FeedbackService_Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace FeedbackService_Domain.Entities
{
    [Table("FEEDBACK")]
    public class Feedback
    {
        public int Id { get; private set; }
        public int IdProduct { get; private set; }
        public GradeEnum Grade { get; private set; }

        public Feedback(int id, int idProduct, GradeEnum grade)
        {
            Id = id;
            IdProduct = idProduct;
            Grade = grade;
        }
    }
}
