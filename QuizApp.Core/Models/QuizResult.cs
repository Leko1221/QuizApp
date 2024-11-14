namespace QuizApp.Core.Models
{
    public class QuizResult
    {
        public Guid Id { get; set; }
        public Guid QuizId { get; set; }
        public Guid UserId { get; set; }
        public int Score { get; set; }
        public DateTime CompletedAt { get; set; }
        public List<QuestionResult> QuestionResults { get; set; }  
    }
}
