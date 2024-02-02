using Tech.Interview.Domain.Entities;

namespace Tech.Interview.Application.Persistence
{
    public interface IUserRepostory
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync();
        Task<Guid> CreateUserAsync(User entity);        
        Task<bool> UpdateUserAsync(Guid userId);
    }
}
