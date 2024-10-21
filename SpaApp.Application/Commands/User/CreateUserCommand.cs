using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpaApp.Application.Responses;

namespace SpaApp.Application.Commands.User
{
    public class CreateUserCommand : IRequest<Response<Domain.Entities.User>>
    {
        public string Username { get; set; }
        public string Email { get; set; }
    }
}
