using Microsoft.EntityFrameworkCore;
using QuizApp.Data.Abstractions;
using QuizApp.Data.ModelDb;
using QuizzApp.Data.Repositories;

namespace QuizApp.Data.Repositories
{
    public class QuizRepository : QuizAppGenericRepository<QuizDbModel>, IQuizRepository
    {
        public QuizRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<QuizDbModel>> GetAllQuizzesWithIncludesAsync()
        {
            return await DbContext.Quizzes
                .Include(q => q.User)
                .Include(q => q.Questions)
                .ThenInclude(q => q.Answers)
                .ToListAsync();
        }

        public async Task<QuizDbModel?> GetQuizWithIncludesIdAsync(Guid? quizId)
        {
            return await DbContext.Quizzes
                .Include(q => q.User)
                .Include(q => q.Questions)
                .ThenInclude(q => q.Answers)
                .FirstOrDefaultAsync(q => q.Id == quizId);
        }
    }
}
