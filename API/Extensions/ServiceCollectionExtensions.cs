﻿using API.Services;

namespace API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            services.AddTransient<ActivityService>();
            services.AddTransient<GroupService>();

            return services;
        }
    }
}
