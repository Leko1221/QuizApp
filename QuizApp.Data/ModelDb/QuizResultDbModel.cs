using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizApp.Data.ModelDb
{
    public class QuizResultDbModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid? Id { get; set; }
        public Guid? UserId { get; set; }
        [ForeignKey("UserId")]
        public UserDbModel? User { get; set; }
        public Guid? QuizId { get; set; }
        [ForeignKey("QuizId")]
        public QuizDbModel? Quiz { get; set; }
        public int? Score { get; set; }
        public DateTime? CompletedAt { get; set; }
    }
}
