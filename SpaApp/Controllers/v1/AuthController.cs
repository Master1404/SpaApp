using MediatR;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using SpaApp.Api.DTO;
using SpaApp.Application.Commands.User;
using SpaApp.Application.Queries.Auth;
using SpaApp.Domain.Entities;
using ExpressMapper.Extensions;

namespace SpaApp.Api.Controllers.v1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [ApiVersion("1.0")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginQuery query)
        {
            try
            {
                var token = await _mediator.Send(query);
                return Ok(new { Token = token });
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserResponseModel>> Register([FromBody] CreateUserCommand command)
        {
            try
            {
                var userResponse = await _mediator.Send(command);
                if (!userResponse.IsSuccessfull)
                    return BadRequest(userResponse.Message);

                var user = userResponse.ResponseValue;

                var responseModel = user.Map<User, UserResponseModel>();  
                return Ok(responseModel);
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
