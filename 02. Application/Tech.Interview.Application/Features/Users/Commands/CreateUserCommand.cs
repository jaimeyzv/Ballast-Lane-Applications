using MediatR;

namespace Tech.Interview.Application.Features.Users.Commands
{
    public class CreateUserCommand : IRequest<bool>
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
    }
}
