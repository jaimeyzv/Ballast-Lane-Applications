using Tech.Interview.Domain.Entities;

namespace Tech.Interview.Application.Persistence
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(Guid userId);
        Task<Guid> CreateUserAsync(User entity);        
        Task<bool> UpdateUserAsync(Guid userId);
    }
}
