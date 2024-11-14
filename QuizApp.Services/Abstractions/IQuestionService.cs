using QuizApp.Core.Models;
using QuizApp.Data.ModelDb;

namespace QuizApp.Services.Abstractions
{
    public interface IQuestionService
    {
        Task<QuestionDbModel> CreateQuestionAsync(Question question);
        Task<bool> DeleteQuestionAsync(Guid questionId);
        Task<IEnumerable<QuestionDbModel>> GetAllQuestionsByQuizIdAsync(Guid quizId);
        Task<QuestionDbModel> GetQuestionByIdAsync(Guid id);
        Task<QuestionDbModel> UpdateQuestionAsync(Guid questionId, Question updatedQuestion);
    }
}