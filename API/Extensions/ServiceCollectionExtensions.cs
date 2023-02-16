using API.Services;

namespace API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            services.AddScoped<ActivityService>();
            services.AddScoped<AuthenticationService>();
            services.AddScoped<BillService>();
            services.AddScoped<CategoryService>();
            services.AddScoped<GroupService>();
            services.AddScoped<ToDoListsService>();

            return services;
        }
    }
}
