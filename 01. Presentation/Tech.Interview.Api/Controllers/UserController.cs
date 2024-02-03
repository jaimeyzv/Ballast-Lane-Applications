using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Tech.Interview.Application.Features.Users.Commands;
using Tech.Interview.Application.Features.Users.Queries;
using Tech.Interview.Application.Presentation;

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
        public async Task<IActionResult> GetAllUsersAsync()
        {
            var result = await _mediator.Send(new GetAllUsersQuery());
            return Ok(result);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserByIdAsync([FromRoute] int userId)
        {
            var result = await _mediator.Send(new GetUserByIdQuery(userId));
            if(result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUserAsync([FromBody] CreateUserViewModel viewModel)
        {
            var commandModel = _mapper.Map<CreateUserCommand>(viewModel);
            var result = await _mediator.Send(commandModel);
            return Ok(result);
        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateUserAsync([FromRoute] int userId,
            [FromBody] UpdateUserViewModel viewModel)
        {
            var userModel = await _mediator.Send(new GetUserByIdQuery(userId));
            if(userModel == null)
                return BadRequest($"User with id {userId} does not exist. Resource can not be updated");
           
            var commandModel = _mapper.Map<UpdateUserCommand>(viewModel);
            commandModel.Id = userId;
            var result = await _mediator.Send(commandModel);
            return Ok(result);
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUserAsync([FromRoute] int userId)
        {
            var userModel = await _mediator.Send(new GetUserByIdQuery(userId));
            if (userModel == null)
                return BadRequest($"User with id {userId} does not exist. Resource can not be deleted");

            var result = await _mediator.Send(new DeleteUserCommand(userId));
            return Ok(result);
        }
    }
}
