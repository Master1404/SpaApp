using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserEntity = SpaApp.Domain.Entities.User;

namespace SpaApp.Application.Queries
{
    public record GetUserByIdQuery : IRequest<UserEntity>
    {
        public string Id { get; set; }
    }
}
