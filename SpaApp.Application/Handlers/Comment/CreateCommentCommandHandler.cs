using MediatR;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using SpaApp.Application.Commands.Comment;
using SpaApp.Application.Commands.User;
using SpaApp.Application.Handlers.User;
using SpaApp.Domain.Repositories;
using SpaApp.Application.Responses;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommentEntity = SpaApp.Domain.Entities.Comment;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace SpaApp.Application.Handlers.Comment
{
    public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, Response<CommentEntity>>
    {
        private readonly ICommentRepository _commentRepository;
        private readonly ILogger<CreateCommentCommandHandler> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CreateCommentCommandHandler(ICommentRepository commentRepository, ILogger<CreateCommentCommandHandler> logger, IHttpContextAccessor httpContextAccessor)
        {
            _commentRepository = commentRepository;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Response<CommentEntity>> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {
            using (_logger.BeginScope("CreateComment {Username} {Email}", /*request.UserName,*/ request.Email))
            {
                _logger.LogInformation("Handling CreateUserCommand");

                var stopwatch = Stopwatch.StartNew();
                var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var userName = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;
              //  var userEmail = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;

                var comment = new CommentEntity
                {
                    Id = ObjectId.GenerateNewId().ToString(),
                    UserId = userId,
                    UserName = userName,
                    Email = request.Email,
                    Text = request.Text,
                    CreatedAt = DateTime.UtcNow,
                    ParentId = ObjectId.GenerateNewId().ToString(),

                };
                await _commentRepository.AddCommentAsync(comment).ConfigureAwait(true);

                stopwatch.Stop();
                _logger.LogInformation("Comment created successfully: {@Comment} (Time taken: {ElapsedMilliseconds}ms)", new { comment.Id, comment.UserName, comment.Email }, stopwatch.ElapsedMilliseconds);

                return new Response<CommentEntity>(comment);
            }
        }
    }
}
