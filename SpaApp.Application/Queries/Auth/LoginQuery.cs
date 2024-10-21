using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaApp.Application.Queries.Auth
{
    public record LoginQuery(string Username, string Password) : IRequest<string>;
}
