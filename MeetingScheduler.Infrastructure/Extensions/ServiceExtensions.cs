using MeetingScheduler.Application.Interfaces;
using MeetingScheduler.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingScheduler.Infrastructure.Extensions
{
    public static class ServiceExtensions
    {
        
        // Registers the infrastructure services with the dependency injection container.
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddSingleton<IUserRepository, UserRepository>();
            services.AddSingleton<IMeetingRepository, MeetingRepository>();

            return services;
        }
    }
}
