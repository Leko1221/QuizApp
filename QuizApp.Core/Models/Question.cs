namespace QuizApp.Core.Models
{
    public class Question
    {
        public Guid? Id { get; set; }
        public string? Text { get; set; }
        public List<Answer>? Answer { get; set; }
        public int? TimeLimitInSeconds { get; set; }
    }
}
