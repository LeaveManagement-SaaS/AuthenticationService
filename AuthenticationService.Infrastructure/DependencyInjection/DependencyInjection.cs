//using AuthenticationService.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
//using AuthenticationService.Infrastructure.Repositories.UserRepository;
using AuthenticationService.Domain.Interfaces;
using AuthenticationService.Infrastructure.Persistence;
using AuthenticationService.Infrastructure.Repositories;

namespace AuthenticationService.Infrastructure.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("Default")));

            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}
