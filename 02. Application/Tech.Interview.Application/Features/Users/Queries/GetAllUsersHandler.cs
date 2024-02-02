using AutoMapper;
using MediatR;
using Tech.Interview.Application.Persistence.UoW;

namespace Tech.Interview.Application.Features.Users.Queries
{
    public class GetAllUsersHandler : IRequestHandler<GetAllUsersQuery, IEnumerable<GetAllUsersModelResult>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllUsersHandler(IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IEnumerable<GetAllUsersModelResult>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            using (var context = _unitOfWork.Create())
            {
                var userEntities = await context.Repositories.UserRepository.GetAllUsersAsync();

                var models = userEntities
                    .Select(x => _mapper.Map<GetAllUsersModelResult>(x))
                    .ToList();

                return models;
            }
        }
    }
}
