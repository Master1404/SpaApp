using MediatR;
using Microsoft.Extensions.Logging;
using SpaApp.Application.Commands.Comment;
using SpaApp.Application.Commands.User;
using SpaApp.Application.Handlers.User;
using SpaApp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaApp.Application.Handlers.Comment
{
    public class DeleteCommentCommandHandler : IRequestHandler<DeleteCommentCommand>
    {
        private readonly ICommentRepository _commentRepository;
        // private readonly IPetRepository _petRepository;
        private readonly ILogger<DeleteCommentCommandHandler> _logger;

        public DeleteCommentCommandHandler(ICommentRepository commentRepository, /*IPetRepository petRepository,*/ ILogger<DeleteCommentCommandHandler> logger)
        {
            _commentRepository = commentRepository ?? throw new ArgumentNullException(nameof(commentRepository));
            //_petRepository = petRepository ?? throw new ArgumentNullException(nameof(petRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
        {
            var deleteUseTask = _commentRepository.DeleteCommentAsync(request.Id);
        }
    }
}
