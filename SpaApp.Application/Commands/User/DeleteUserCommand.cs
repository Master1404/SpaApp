using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaApp.Application.Commands.User
{
    public record DeleteUserCommand(string Id) : IRequest;
}
