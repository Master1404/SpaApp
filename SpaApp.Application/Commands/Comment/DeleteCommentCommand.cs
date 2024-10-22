using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaApp.Application.Commands.Comment
{
    public record DeleteCommentCommand(string Id) : IRequest;
}
