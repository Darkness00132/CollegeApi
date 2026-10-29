using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace College.Infrastructure
{
    public static class DepenencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            return services;
        }
    }
}
