using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Planner.Infrastructure.Data.EntityFramework;

namespace Planner.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContextPool<PlannerDbContext>(opt =>
            {
                opt.UseSqlServer(
                    configuration.GetConnectionString("SQLServer"), 
                    b => b.MigrationsAssembly(typeof(PlannerDbContext).Assembly.FullName));
            });
            return services;
        }
    }
}