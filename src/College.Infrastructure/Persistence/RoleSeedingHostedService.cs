using System.Reflection;
using College.Application.Common.Constants;
using College.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace College.Infrastructure.Persistence
{
    /// <summary>
    /// Ensures every role from <see cref="Roles"/> exists when the application starts.
    /// </summary>
    public sealed class RoleSeedingHostedService(
        IServiceProvider serviceProvider,
        ILogger<RoleSeedingHostedService> logger) : IHostedService
    {
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = serviceProvider.CreateScope();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<AppRole>>();

            var roleNames = typeof(Roles)
                .GetFields(BindingFlags.Public | BindingFlags.Static)
                .Where(field => field.IsLiteral && field.FieldType == typeof(string))
                .Select(field => (string)field.GetValue(null)!)
                .ToList();

            foreach (var roleName in roleNames)
            {
                if (await roleManager.RoleExistsAsync(roleName))
                {
                    continue;
                }

                var result = await roleManager.CreateAsync(new AppRole { Name = roleName });
                if (!result.Succeeded)
                {
                    logger.LogError(
                        "Failed to seed role {RoleName}: {Errors}",
                        roleName,
                        string.Join("; ", result.Errors.Select(e => e.Description)));
                }
            }

            logger.LogInformation("Role seeding completed. {Count} roles ensured.", roleNames.Count);
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
