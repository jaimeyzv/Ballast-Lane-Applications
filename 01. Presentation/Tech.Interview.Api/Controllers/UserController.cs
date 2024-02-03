using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Tech.Interview.Application.Features.Users.Queries;

namespace Tech.Interview.Api.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public UserController(IMediator mediator,
            IMapper mapper)
        {
            this._mediator = mediator;
            this._mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var result = await _mediator.Send(new GetAllUsersQuery());
            return Ok(result);
        }
    }
}
