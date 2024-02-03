using MediatR;
using Tech.Interview.Application.Persistence.UoW;

namespace Tech.Interview.Application.Features.Users.Commands
{
    public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteUserHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            using (var context = _unitOfWork.Create())
            {
                await context.Repositories.UserRepository.DeleteUserAsync(request.UserId);
                await context.SaveChangesAcync();
                return true;
            }
        }
    }
}
