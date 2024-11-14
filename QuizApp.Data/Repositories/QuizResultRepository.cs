using QuizApp.Data.Abstractions;
using QuizApp.Data.ModelDb;
using QuizzApp.Data.Repositories;

namespace QuizApp.Data.Repositories
{
    public class QuizResultRepository : QuizAppGenericRepository<QuizResultDbModel>, IQuizResultRepository
    {
        public QuizResultRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
