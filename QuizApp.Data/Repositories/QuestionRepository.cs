using Microsoft.EntityFrameworkCore;
using QuizApp.Data.Abstractions;
using QuizApp.Data.ModelDb;
using QuizzApp.Data.Repositories;

namespace QuizApp.Data.Repositories
{
    public class QuestionRepository : QuizAppGenericRepository<QuestionDbModel>, IQuestionRepository
    {
        public QuestionRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<QuestionDbModel?> GetQuestionWithAnswersAsync(Guid? questionId)
        {
            return await DbContext.Questions
                .Include(q => q.Answers)
                .FirstOrDefaultAsync(q => q.Id == questionId);
        }
    }
}

