using QuizApp.Core.Models;

public interface IUserService
{
    Task<User> CreateUserAsync(User userViewModel);
    Task<bool> DeleteUserAsync(Guid id);
    Task<IEnumerable<User>> GetAllUsersAsync();
    Task<User> GetUserByIdAsync(Guid id);
    Task<bool> UpdateUserAsync(Guid id, User updatedUser);
}