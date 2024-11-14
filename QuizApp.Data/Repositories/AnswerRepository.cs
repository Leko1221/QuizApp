using QuizApp.Data.Abstractions;
using QuizApp.Data.ModelDb;
using QuizzApp.Data.Repositories;

namespace QuizApp.Data.Repositories
{
    public class AnswerRepository : QuizAppGenericRepository<AnswerDbModel>, IAnswerRepository
    {
        public AnswerRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
