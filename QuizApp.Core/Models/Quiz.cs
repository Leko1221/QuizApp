namespace QuizApp.Core.Models
{
    public class Quiz
    {
        public Guid? Id { get; set; }
        public string Title { get; set; }
        public User User { get; set; }  
        public List<Question> Questions { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}