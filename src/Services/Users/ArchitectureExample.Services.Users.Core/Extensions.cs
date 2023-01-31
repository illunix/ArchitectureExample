using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Phaeton.DAL.Postgres;
using Phaeton.DependencyInjection;

namespace ArchitectureExample.Services.Users.Core;

public static class Extensions
{
    public static IServiceCollection AddCore(
        this IServiceCollection services,
        IConfiguration config
    )
        => services
            .RegisterServicesFromAssembly()
            .AddPostgres(config);
}