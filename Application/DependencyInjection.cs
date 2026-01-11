using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application // <--- OJO AQUÍ, debe ser solo "Application"
{
    public static class DependencyInjection // <--- Debe ser public static
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            return services;
        }
    }
}