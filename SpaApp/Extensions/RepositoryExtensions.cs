﻿using SpaApp.Domain.Repositories;
using SpaApp.Infrastrucrure.Repositories;

namespace SpaApp.Api.Extensions
{
    public static class RepositoryExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}
