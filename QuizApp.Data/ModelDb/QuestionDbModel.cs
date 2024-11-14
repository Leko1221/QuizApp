using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizApp.Data.ModelDb
{
    public class QuestionDbModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid? Id { get; set; }
        public string? Text { get; set; }
        public Guid? QuizId { get; set; }
        [ForeignKey("QuizId")]
        public QuizDbModel? Quiz { get; set; }
        [ForeignKey("AnswerId")]
        public ICollection<AnswerDbModel>? Answers { get; set; }
        public int? TimeLimitInSeconds { get; set; }
    }
}
