using MediatR;
using SpaApp.Application.Interfaces;
using SpaApp.Application.Queries.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaApp.Application.Handlers.Auth
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, string>
    {
        private readonly IAuthService _authService;

        public LoginQueryHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<string> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var token = await _authService.AuthenticateAsync(request.Username, request.Password);
            if (string.IsNullOrEmpty(token))
                throw new UnauthorizedAccessException("Invalid username or password");

            return token;
        }
    }
}
