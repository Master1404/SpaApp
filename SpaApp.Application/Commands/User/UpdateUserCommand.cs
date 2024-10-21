using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpaApp.Application.Responses;
using UserEntity = SpaApp.Domain.Entities.User;

namespace SpaApp.Application.Commands.User
{
    public record UpdateUserCommand : IRequest<Response<UserEntity>>
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
