using MediatR;

namespace Tech.Interview.Application.Features.Users.Commands
{
    public class DeleteUserCommand : IRequest<bool>
    {
        public DeleteUserCommand(int userId)
        {
            this.UserId = userId;
        }
        public int UserId { get; set; }
    }
}
