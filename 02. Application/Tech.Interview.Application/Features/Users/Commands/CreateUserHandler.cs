using AutoMapper;
using MediatR;
using Tech.Interview.Application.Persistence.UoW;
using Tech.Interview.Domain.Entities;

namespace Tech.Interview.Application.Features.Users.Commands
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateUserHandler(IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            using (var context = _unitOfWork.Create())
            {
                var entity = _mapper.Map<User>(request);
                await context.Repositories.UserRepository.CreateUserAsync(entity);
                await context.SaveChangesAcync();
                return true;
            }
        }
    }
}
