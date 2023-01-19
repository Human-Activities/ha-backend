using API.Services;

namespace API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            services.AddScoped<ActivityService>();
            services.AddScoped<AuthenticationService>();
            services.AddScoped<GroupService>();
            services.AddScoped<ToDoListsService>();

            return services;
        }
    }
}
