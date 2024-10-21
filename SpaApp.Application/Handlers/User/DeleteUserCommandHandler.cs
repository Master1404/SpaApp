using MediatR;
using Microsoft.Extensions.Logging;
using SpaApp.Application.Commands.User;
using SpaApp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaApp.Application.Handlers.User
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
    {
        private readonly IUserRepository _userRepository;
       // private readonly IPetRepository _petRepository;
        private readonly ILogger<DeleteUserCommandHandler> _logger;

        public DeleteUserCommandHandler(IUserRepository userRepository, /*IPetRepository petRepository,*/ ILogger<DeleteUserCommandHandler> logger)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
           //_petRepository = petRepository ?? throw new ArgumentNullException(nameof(petRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
                var deleteUseTask = _userRepository.DeleteUserAsync(request.Id);
        }
    }
}
