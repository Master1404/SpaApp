using MediatR;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpaApp.Application.Commands.User;
using SpaApp.Application.Queries;
using SpaApp.Domain.Entities;
using System.Security.Claims;
using SpaApp.Api.DTO;
using SpaApp.Application.Commands.User;
using SpaApp.Application.Queries;
using SpaApp.Domain.Entities;
using System.Security.Claims;
using ExpressMapper.Extensions;

namespace SpaApp.Api.Controllers.v1
{

    [ApiController]
    [Authorize] // This applies authorization to all endpoints in this controller
    [Route("api/v1/[controller]")]
    [ApiVersion("1.0")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("list")]
        public async Task<ActionResult<IEnumerable<UserResponseModel>>> GetUserList()
        {
            var users = await _mediator.Send(new GetUserListQuery());
            if (users == null)
                return NotFound();

            var userDtos = users.Map<IEnumerable<User>, IEnumerable<UserResponseModel>>();
            return Ok(userDtos);
        }

        [HttpGet("me")]
        public async Task<IActionResult> GetCurrentUser()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
                return NotFound();

            var user = await _mediator.Send(new GetUserByIdQuery { Id = userId });
            if (user == null)
                return NotFound();

            var responseModel = user.Map<User, UserResponseModel>();

            return Ok(responseModel);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserResponseModel>> GetUserById(string id)
        {
            var user = await _mediator.Send(new GetUserByIdQuery { Id = id });
            if (user == null)
                return NotFound();
            var responseModel = user.Map<User, UserResponseModel>();

            return Ok(responseModel);
        }

        [HttpPost]
        public async Task<ActionResult<UserResponseModel>> CreateUser([FromBody] CreateUserCommand command)
        {
            var userResponse = await _mediator.Send(command);
            if (!userResponse.IsSuccessfull)
                return BadRequest(userResponse.Message);

            var user = userResponse.ResponseValue;

            var responseModel = user.Map<User, UserResponseModel>();
            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
        }

       /* [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] UpdateUserCommand command)
        {
            if (id != command.Id)
                return BadRequest();

            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            await _mediator.Send(new DeleteUserCommand(id));
            return Accepted();
        }*/
    }
}
