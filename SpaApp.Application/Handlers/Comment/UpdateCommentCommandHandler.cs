using MediatR;
using Microsoft.Extensions.Logging;
using SpaApp.Application.Commands.Comment;
using SpaApp.Application.Commands.User;
using SpaApp.Application.Common;
using SpaApp.Application.Handlers.User;
using SpaApp.Domain.Repositories;
using System;
using System.Collections.Generic;
using SpaApp.Application.Responses;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommentEntity = SpaApp.Domain.Entities.Comment;

namespace SpaApp.Application.Handlers.Comment
{
    public class UpdateCommentCommandHandler : IRequestHandler<UpdateCommentCommand, Response<CommentEntity>>
    {
        private readonly ICommentRepository _commentRepository;
        //private readonly IValidator<UserEntity> _validator;
        private readonly ILogger<UpdateCommentCommandHandler> _logger;

        public UpdateCommentCommandHandler(ICommentRepository commentRepository,/* IValidator<UserEntity> validator,*/ ILogger<UpdateCommentCommandHandler> logger)
        {
            _commentRepository = commentRepository ?? throw new ArgumentNullException(nameof(commentRepository));
            // _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Response<CommentEntity>> Handle(UpdateCommentCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling UpdateCommentCommand for comment with ID: {CommentId}", request.Id);

            var existingComment = await _commentRepository.GetCommentByIdAsync(request.Id);
            if (existingComment == null)
            {
                _logger.LogError("Comment with ID {Id} was not found.", request.Id);
                return new Response<CommentEntity>($"Comment with ID {request.Id} was not found.");
            }

            //existingComment.UserName = request.UserName;
            //existingComment.Email = request.Email;
            existingComment.Text = request.Text;
            existingComment.CreatedAt = DateTime.UtcNow;

            /* var validationResult = await _validator.ValidateAsync(existingUser, cancellationToken);
             if (!validationResult.IsValid)
             {
                 var errorMessages = string.Join(' ', validationResult.Errors);
                 _logger.LogError("Validation failed for user update: {Errors}", validationResult.Errors);
                 return new Response<UserEntity>(errorMessages);
             }*/

            await _commentRepository.UpdateCommentAsync(existingComment);
            _logger.LogInformation("Successfully updated user with ID: {UserId}", request.Id);

            return new Response<CommentEntity>(existingComment);
        }
    }
}
