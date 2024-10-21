using MediatR;
using Microsoft.Extensions.Logging;
using SpaApp.Application.Commands.User;
using SpaApp.Domain.Repositories;
using SpaApp.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserEntity = SpaApp.Domain.Entities.User;
using SpaApp.Application.Common;

namespace SpaApp.Application.Handlers.User
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Response<UserEntity>>
    {
        private readonly IUserRepository _userRepository;
        //private readonly IValidator<UserEntity> _validator;
        private readonly ILogger<UpdateUserCommandHandler> _logger;

        public UpdateUserCommandHandler(IUserRepository userRepository,/* IValidator<UserEntity> validator,*/ ILogger<UpdateUserCommandHandler> logger)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
           // _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Response<UserEntity>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling UpdateUserCommand for user with ID: {UserId}", request.Id);

            var existingUser = await _userRepository.GetUserByIdAsync(request.Id);
            if (existingUser == null)
            {
                _logger.LogError("User with ID {Id} was not found.", request.Id);
                return new Response<UserEntity>($"User with ID {request.Id} was not found.");
            }

            existingUser.Username = request.Username;
            existingUser.Email = request.Email;

            if (!string.IsNullOrEmpty(request.Password))
            {
                _logger.LogInformation("Updating password for user with ID: {UserId}", request.Id);
                existingUser.Password = HashUtility.HashPassword(request.Password);
            }

            existingUser.Username = request.Username;
            existingUser.Email = request.Email;

           /* var validationResult = await _validator.ValidateAsync(existingUser, cancellationToken);
            if (!validationResult.IsValid)
            {
                var errorMessages = string.Join(' ', validationResult.Errors);
                _logger.LogError("Validation failed for user update: {Errors}", validationResult.Errors);
                return new Response<UserEntity>(errorMessages);
            }*/

            await _userRepository.UpdateUserAsync(existingUser);
            _logger.LogInformation("Successfully updated user with ID: {UserId}", request.Id);

            return new Response<UserEntity>(existingUser);
        }
    }
}
