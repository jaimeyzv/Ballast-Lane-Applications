using Tech.Interview.Application.Persistence.UoW;
using Tech.Interview.Application.Services;
using Tech.Interview.Domain.Entities;

namespace Tech.Interview.Service
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task CreateUserAsync(User entity)
        {
            using (var context = _unitOfWork.Create())
            {
                await context.Repositories.UserRepository.CreateUserAsync(entity);
                await context.SaveChangesAcync();
            }
        }

        public async Task DeleteUserAsync(int userId)
        {
            using (var context = _unitOfWork.Create())
            {
                await context.Repositories.UserRepository.DeleteUserAsync(userId);
                await context.SaveChangesAcync();
            }
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            using (var context = _unitOfWork.Create())
            {
                return await context.Repositories.UserRepository.GetAllUsersAsync();
            }
        }

        public async Task<User> GetUserByIdAsync(int userId)
        {
            using (var context = _unitOfWork.Create())
            {
                return await context.Repositories.UserRepository.GetUserByIdAsync(userId);
            }
        }

        public async Task UpdateUserAsync(User entity)
        {
            using (var context = _unitOfWork.Create())
            {
                await context.Repositories.UserRepository.UpdateUserAsync(entity);
                await context.SaveChangesAcync();
            }
        }
    }
}
