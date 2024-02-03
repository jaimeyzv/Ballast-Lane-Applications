using AutoMapper;
using MediatR;
using Tech.Interview.Application.Persistence.UoW;

namespace Tech.Interview.Application.Features.Users.Queries
{
    public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, GetUserByIdModelResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetUserByIdHandler(IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GetUserByIdModelResult> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            using (var context = _unitOfWork.Create())
            {
                var userEntity = await context.Repositories.UserRepository.GetUserByIdAsync(request.UserId);
                if (userEntity == null) return null;
                var model = _mapper.Map<GetUserByIdModelResult>(userEntity);

                return model;
            }
        }
    }
}
