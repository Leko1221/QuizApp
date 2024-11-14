using Microsoft.EntityFrameworkCore;
using QuizApp.Data.Abstractions;
using QuizApp.Data.ModelDb;
using QuizzApp.Data.Repositories;

namespace QuizApp.Data.Repositories
{
    public class UserRepository : QuizAppGenericRepository<UserDbModel>, IUserRepository
    {
        public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<UserDbModel?> GetUserWithIncludesIdAsync(Guid? userId)
        {
            return await DbContext.Users
                .AsNoTracking() 
                .FirstOrDefaultAsync(x => x.Id == userId);
        }
    }
}
