using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizApp.Data.ModelDb
{
    public class QuizDbModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid? Id { get; set; }
        public string? Title { get; set; }
        public Guid? UserId { get; set; }
        public Guid? QuestionId { get; set; }
        [ForeignKey("UserId")]
        public UserDbModel? User { get; set; }
        [ForeignKey("QuestionId")]
        public ICollection<QuestionDbModel> Questions { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
