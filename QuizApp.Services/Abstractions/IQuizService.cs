using QuizApp.Core.Models;
using QuizApp.Data.ModelDb;

namespace QuizApp.Services.Abstractions
{
    public interface IQuizService
    {
        Task<QuizDbModel> CreateQuizAsync(Quiz quiz);
        Task<bool> DeleteQuizAsync(Guid quizId);
        Task<List<Quiz>> GetAllQuizzesAsync();
        Task<Quiz> GetQuizByIdAsync(Guid? quizId);
    }
}