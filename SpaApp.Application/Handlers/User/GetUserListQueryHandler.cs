using MediatR;
using SpaApp.Application.Queries;
using SpaApp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserEntity = SpaApp.Domain.Entities.User;

namespace SpaApp.Application.Handlers.User
{
    public class GetUserListQueryHandler : IRequestHandler<GetUserListQuery, IEnumerable<UserEntity>>
    {
        private readonly IUserRepository _userRepository;

        public GetUserListQueryHandler(IUserRepository userRepository) => _userRepository = userRepository;

        public async Task<IEnumerable<UserEntity>> Handle(GetUserListQuery request, CancellationToken cancellationToken)
        {
            return await _userRepository.GetAllUsersAsync();
        }
    }
}
