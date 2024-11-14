using Microsoft.EntityFrameworkCore;
using QuizApp.Core.Models;
using QuizApp.Data;
using QuizApp.Data.Abstractions;
using QuizApp.Data.ModelDb;

namespace QuizApp.Services.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserRepository _userRepository;

        public UserService(ApplicationDbContext context, IUserRepository userRepository)
        {
            _context = context;
            _userRepository = userRepository;
        }

        public async Task<User> CreateUserAsync(User userViewModel)
        {
            // moze da se uradi ovde jednostavno mapiranje db modela u view model
            var userDbModel = new UserDbModel
            {
                Id = Guid.NewGuid(),
                Email = userViewModel.Email,
                PasswordHash = userViewModel.PasswordHash,
                DisplayName = userViewModel.DisplayName
            };

            _context.Users.Add(userDbModel);
            await _context.SaveChangesAsync();

            // Map back to view model for returning response
            return new User
            {
                Id = (Guid)userDbModel.Id,
                Email = userDbModel.Email,
                DisplayName = userDbModel.DisplayName,
                PasswordHash = userDbModel.PasswordHash
            };
        }

        public async Task<User> GetUserByIdAsync(Guid id)
        {
            // takodje moze da se uradi jednostavno mapiranje nakon pronalazenje usera
            var userDbModel = await _context.Users.FindAsync(id);
            if (userDbModel == null) return null;

            // Map to view model
            return new User
            {
                Id = (Guid)userDbModel.Id,
                Email = userDbModel.Email,
                DisplayName = userDbModel.DisplayName,
                PasswordHash = userDbModel.PasswordHash
            };
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            var usersDb = await _context.Users.ToListAsync();

            // takodje moze da se uradi jednostavno mapiranje nakon pronalazenja svih usera
            return usersDb.Select(userDb => new User
            {
                Id = (Guid)userDb.Id,
                Email = userDb.Email,
                DisplayName = userDb.DisplayName,
                PasswordHash = userDb.PasswordHash
            });
        }

        public async Task<bool> UpdateUserAsync(Guid id, User updatedUser)
        {
            var userDbModel = await _userRepository.GetByIdAsync(updatedUser.Id);
            if (userDbModel == null) return false;

            // Update fields
            userDbModel.Email = updatedUser.Email;
            userDbModel.DisplayName = updatedUser.DisplayName;
            userDbModel.PasswordHash = updatedUser.PasswordHash;

            _context.Users.Update(userDbModel);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteUserAsync(Guid id)
        {
            var userDbModel = await _context.Users.FindAsync(id);
            if (userDbModel == null) return false;

            _context.Users.Remove(userDbModel);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
