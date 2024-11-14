using QuizApp.Data.ModelDb;

namespace QuizApp.Data.Abstractions
{
    public interface IQuizRepository : IQuizAppGenericRepository<QuizDbModel>
    {
        Task<QuizDbModel?> GetQuizWithIncludesIdAsync(Guid? quizId);
        Task<List<QuizDbModel>> GetAllQuizzesWithIncludesAsync();
    }
}



