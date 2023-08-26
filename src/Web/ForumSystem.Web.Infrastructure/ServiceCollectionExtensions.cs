namespace ForumSystem.Web.Infrastructure
{
    using System.Linq;
    using System.Reflection;

    using ForumSystem.Common;
    using Microsoft.Extensions.DependencyInjection;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddConventionalServices(
            this IServiceCollection services,
            params Assembly[] assemblies)
        {
            var serviceInterfaceType = typeof(IService);
            var singletonServiceInterfaceType = typeof(ISingletonService);
            var scopedServiceInterfaceType = typeof(IScopedService);

            foreach (var assembly in assemblies)
            {
                var types = assembly
                    .GetExportedTypes()
                    .Where(t => t.IsClass && !t.IsAbstract)
                    .Select(t => new
                    {
                        Interface = t.GetInterface($"I{t.Name}"),
                        Implementation = t,
                    })
                    .Where(t => t.Interface != null);

                foreach (var type in types)
                {
                    if (serviceInterfaceType.IsAssignableFrom(type.Interface))
                    {
                        services.AddTransient(type.Interface, type.Implementation);
                    }
                    else if (singletonServiceInterfaceType.IsAssignableFrom(type.Interface))
                    {
                        services.AddSingleton(type.Interface, type.Implementation);
                    }
                    else if (scopedServiceInterfaceType.IsAssignableFrom(type.Interface))
                    {
                        services.AddScoped(type.Interface, type.Implementation);
                    }
                }
            }

            return services;
        }
    }
}
