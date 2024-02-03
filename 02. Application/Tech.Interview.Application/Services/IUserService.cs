using Tech.Interview.Domain.Entities;

namespace Tech.Interview.Application.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(int userId);
        Task CreateUserAsync(User entity);
        Task UpdateUserAsync(User entity);
        Task DeleteUserAsync(int userId);
    }
}
