using System.Reflection;
using MediatR;
using SpaApp.Application.Commands;
using SpaApp.Application.Commands.User;
using SpaApp.Application.Handlers;
using SpaApp.Application.Handlers.Auth;
using SpaApp.Application.Handlers.User;
using SpaApp.Application.Interfaces;
using SpaApp.Application.Queries.Auth;
using SpaApp.Application.Queries;
using SpaApp.Application.Responses;
using SpaApp.Application.Services;
using SpaApp.Domain.Entities;

namespace SpaApp.Api.Extensions
{
    public static class CqrsExtensions
    {
        public static IServiceCollection AddCqrsHandlers(this IServiceCollection services)
        {
            // auth
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped(typeof(IRequestHandler<LoginQuery, string>), typeof(LoginQueryHandler));

            // user
             services.AddScoped(typeof(IRequestHandler<GetUserListQuery, IEnumerable<User>>), typeof(GetUserListQueryHandler));
            //  services.AddScoped(typeof(IRequestHandler<GetUserByIdQuery, User>), typeof(GetUserByIdQueryHandler));
            services.AddScoped(typeof(IRequestHandler<CreateUserCommand, Response<User>>), typeof(CreateUserCommandHandler));
            // services.AddScoped(typeof(IRequestHandler<UpdateUserCommand, Response<User>>), typeof(UpdateUserCommandHandler));
            //services.AddScoped(typeof(IRequestHandler<DeleteUserCommand>), typeof(DeleteUserCommandHandler));




            return services;
        }
    }
}
