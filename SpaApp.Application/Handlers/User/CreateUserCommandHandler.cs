using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using SpaApp.Application.Responses;
using SpaApp.Domain.Repositories;
using UserEntity = SpaApp.Domain.Entities.User;
using SpaApp.Application.Commands.User;
using SpaApp.Application.Commands.User;
using SpaApp.Domain.Repositories;
using MongoDB.Bson;

namespace SpaApp.Application.Handlers.User
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Response<UserEntity>>
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<CreateUserCommandHandler> _logger;
        //test
        public CreateUserCommandHandler(IUserRepository userRepository, ILogger<CreateUserCommandHandler> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task<Response<UserEntity>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            using (_logger.BeginScope("CreateUser {Username} {Email}", request.Username, request.Email))
            {
                _logger.LogInformation("Handling CreateUserCommand");

                var stopwatch = Stopwatch.StartNew();

                var user = new UserEntity
                {
                    Id = ObjectId.GenerateNewId().ToString(),
                    Username = request.Username,
                    Email = request.Email,
                    Password = request.Password,
                };

                await _userRepository.AddUserAsync(user).ConfigureAwait(true);

                stopwatch.Stop();
                _logger.LogInformation("User created successfully: {@User} (Time taken: {ElapsedMilliseconds}ms)", new { user.Id, user.Username, user.Email }, stopwatch.ElapsedMilliseconds);

                return new Response<UserEntity>(user);
            }
        }
    }
}
