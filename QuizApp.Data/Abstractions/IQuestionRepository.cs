using QuizApp.Data.ModelDb;

namespace QuizApp.Data.Abstractions
{
    public interface IQuestionRepository : IQuizAppGenericRepository<QuestionDbModel>
    {
        Task<QuestionDbModel?> GetQuestionWithAnswersAsync(Guid? questionId);
    }
}
