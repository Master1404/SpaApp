using System.Reflection;
using MediatR;
using SpaApp.Application.Commands;
using SpaApp.Application.Commands.User;
using SpaApp.Application.Commands.Comment;
using SpaApp.Application.Handlers;
using SpaApp.Application.Handlers.Auth;
using SpaApp.Application.Handlers.User;
using SpaApp.Application.Handlers.Comment;
using SpaApp.Application.Interfaces;
using SpaApp.Application.Queries.Auth;
using SpaApp.Application.Queries;
using SpaApp.Application.Responses;
using SpaApp.Application.Services;
using SpaApp.Domain.Entities;
using SpaApp.Application.Queries.Comment;

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
            services.AddScoped(typeof(IRequestHandler<GetUserByIdQuery, User>), typeof(GetUserByIdQueryHandler));
            services.AddScoped(typeof(IRequestHandler<CreateUserCommand, Response<User>>), typeof(CreateUserCommandHandler));
            services.AddScoped(typeof(IRequestHandler<UpdateUserCommand, Response<User>>), typeof(UpdateUserCommandHandler));
            services.AddScoped(typeof(IRequestHandler<DeleteUserCommand>), typeof(DeleteUserCommandHandler));

            // comment
            services.AddScoped(typeof(IRequestHandler<GetCommentListQuery, IEnumerable<Comment>>), typeof(GetCommentListQueryHandler));
            services.AddScoped(typeof(IRequestHandler<GetCommentByIdQuery, Comment>), typeof(GetCommentByIdQueryHandler));
            services.AddScoped(typeof(IRequestHandler<CreateCommentCommand, Response<Comment>>), typeof(CreateCommentCommandHandler));
            services.AddScoped(typeof(IRequestHandler<UpdateCommentCommand, Response<Comment>>), typeof(UpdateCommentCommandHandler));
            services.AddScoped(typeof(IRequestHandler<DeleteCommentCommand>), typeof(DeleteCommentCommandHandler));

            services.AddHttpContextAccessor();


            return services;
        }
    }
}
