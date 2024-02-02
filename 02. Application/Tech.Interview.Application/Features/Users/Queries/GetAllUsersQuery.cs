using MediatR;

namespace Tech.Interview.Application.Features.Users.Queries
{
    public class GetAllUsersQuery: IRequest<IEnumerable<GetAllUsersModelResult>>
    {
    }
}
