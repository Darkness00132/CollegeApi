namespace College.Infrastructure
{
    public static class DepenencyInjection
    {
        public static IServiceCollection AddApi(this IServiceCollection services, IConfiguration configuration)
        {
            return services;
        }
    }
}
