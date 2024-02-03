using AutoMapper;
using MediatR;
using Tech.Interview.Application.Persistence.UoW;
using Tech.Interview.Domain.Entities;

namespace Tech.Interview.Application.Features.Users.Commands
{
    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateUserHandler(IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            using (var context = _unitOfWork.Create())
            {
                var entity = _mapper.Map<User>(request);
                await context.Repositories.UserRepository.UpdateUserAsync(entity);
                await context.SaveChangesAcync();
                return true;
            }
        }
    }
}
