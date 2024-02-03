﻿using AutoMapper;
using Tech.Interview.Application.Persistence.UoW;
using Tech.Interview.Domain.Entities;

namespace Tech.Interview.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
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