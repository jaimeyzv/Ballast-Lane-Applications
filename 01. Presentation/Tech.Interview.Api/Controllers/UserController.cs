using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Tech.Interview.Application.Presentation;
using Tech.Interview.Application.Services;
using Tech.Interview.Domain.Entities;

namespace Tech.Interview.Api.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService,
            IMapper mapper)
        {
            this._userService = userService;
            this._mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsersAsync()
        {
            var result = await this._userService.GetAllUsersAsync();
            return Ok(result);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserByIdAsync([FromRoute] int userId)
        {
            var result = await this._userService.GetUserByIdAsync(userId);
            if(result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUserAsync([FromBody] CreateUserViewModel viewModel)
        {
            var model = _mapper.Map<User>(viewModel);
            await this._userService.CreateUserAsync(model);
            return Ok();
        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateUserAsync([FromRoute] int userId,
            [FromBody] UpdateUserViewModel viewModel)
        {
            var userModel = await this._userService.GetUserByIdAsync(userId);
            if (userModel == null)
                return BadRequest($"User with id {userId} does not exist. Resource can not be updated");
           
            var model = _mapper.Map<User>(viewModel);
            model.UserId = userId;
            await this._userService.UpdateUserAsync(model);

            return Ok();
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUserAsync([FromRoute] int userId)
        {
            var userModel = await this._userService.GetUserByIdAsync(userId);
            if (userModel == null)
                return BadRequest($"User with id {userId} does not exist. Resource can not be deleted");

            await this._userService.DeleteUserAsync(userId);
            return Ok();
        }
    }
}
