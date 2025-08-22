using TodoAPI.Repositories;
using TodoAPI.Services.Implementations;
using TodoAPI.Services.Interfaces;

namespace TodoAPI.Helpers
{
    public static class ServiceInjection
    {
        public static void InjectService(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ITodoService, TodoService>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));


        }
    }
}
