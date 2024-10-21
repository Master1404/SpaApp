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
using SpaApp.DTO;
using SpaApp.Application.Queries.Comment;
using SpaApp.Application.Commands.Comment;

namespace SpaApp.Controllers.v1
{
    [ApiController]
    [Authorize] // This applies authorization to all endpoints in this controller
    [Route("api/v1/[controller]")]
    [ApiVersion("1.0")]
    public class CommentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CommentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("list")]
        public async Task<ActionResult<IEnumerable<CommentResponceModel>>> GetCommentList()
        {
            var comment = await _mediator.Send(new GetCommentListQuery());
            if (comment == null)
                return NotFound();

            var commentDtos = comment.Map<IEnumerable<Comment>, IEnumerable<CommentResponceModel>>();
            return Ok(commentDtos);
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
        public async Task<ActionResult<CommentResponceModel>> GetCommentById(string id)
        {
            var comment = await _mediator.Send(new GetCommentByIdQuery { Id = id });
            if (comment == null)
                return NotFound();
            var responseModel = comment.Map<Comment, CommentResponceModel>();

            return Ok(responseModel);
        }

        [HttpPost]
        public async Task<ActionResult<CommentResponceModel>> CreateComment([FromBody] CreateCommentCommand command)
        {
            var commentResponse = await _mediator.Send(command);
            if (!commentResponse.IsSuccessfull)
                return BadRequest(commentResponse.Message);

            var comment = commentResponse.ResponseValue;

            var responseModel = comment.Map<Comment, CommentResponceModel>();
            return CreatedAtAction(nameof(GetCommentById), new { id = comment.Id },comment);
        }

        [HttpPut("{id}")]
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
        }
    }
}
