using Tech.Interview.Domain.Entities;

namespace Tech.Interview.Application.Persistence
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(int userId);
        Task CreateUserAsync(User entity);        
        Task UpdateUserAsync(User entity);
    }
}
