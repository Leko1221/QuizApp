using QuizApp.Core.Models;
using QuizApp.Data.ModelDb;

namespace QuizApp.Services.Abstractions
{
    public interface IAnswerService
    {
        Task<AnswerDbModel> CreateAnswerAsync(Answer answer);
        Task DeleteAnswerAsync(Guid answerId);
        Task<IEnumerable<AnswerDbModel>> GetAllAnswersByQuestionIdAsync(Guid questionId);
        Task<AnswerDbModel> UpdateAnswerAsync(Guid answerId, Answer updatedAnswer);
        Task<AnswerDbModel> GetAnswerByIdAsync(Guid id);
    }
}