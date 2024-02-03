using MediatR;

namespace Tech.Interview.Application.Features.Users.Queries
{
    public class GetUserByIdQuery : IRequest<GetUserByIdModelResult>
    {
        public GetUserByIdQuery(int userId)
        {
            this.UserId = userId;
        }

        public int UserId { get; set; }
    }
}
