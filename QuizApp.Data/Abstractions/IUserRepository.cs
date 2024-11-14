using QuizApp.Data.ModelDb;

namespace QuizApp.Data.Abstractions
{
    public interface IUserRepository : IQuizAppGenericRepository<UserDbModel>
    {
        Task<UserDbModel?> GetUserWithIncludesIdAsync(Guid? userId);
    }
}
