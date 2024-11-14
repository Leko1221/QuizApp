namespace QuizApp.Core.Models
{
    public class Answer
    {
        public Guid? Id { get; set; }
        public string? Text { get; set; }
        public bool? IsCorrect { get; set; }
    }
}
