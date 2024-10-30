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

namespace SpaApp.Application.Handlers.Comment
{
    public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, Response<CommentEntity>>
    {
        private readonly ICommentRepository _commentRepository;
        private readonly ILogger<CreateCommentCommandHandler> _logger;

        public CreateCommentCommandHandler(ICommentRepository commentRepository, ILogger<CreateCommentCommandHandler> logger)
        {
            _commentRepository = commentRepository;
            _logger = logger;
        }

        public async Task<Response<CommentEntity>> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {
            using (_logger.BeginScope("CreateComment {Username} {Email}", request.UserName, request.Email))
            {
                _logger.LogInformation("Handling CreateUserCommand");

                var stopwatch = Stopwatch.StartNew();

                var comment = new CommentEntity
                {
                    Id = ObjectId.GenerateNewId().ToString(),
                    UserName = request.UserName,
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
