using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizApp.Data.ModelDb
{
    public class AnswerDbModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid? Id { get; set; }
        public string? Text { get; set; }
        public bool? IsCorrect { get; set; }
        public Guid? QuestionId { get; set; }
        [ForeignKey("QuestionId")]
        public QuestionDbModel? Question { get; set; }
    }
}
