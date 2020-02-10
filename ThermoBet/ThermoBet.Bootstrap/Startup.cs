using System;
using ThermoBet.Data;
using ThermoBet.Data.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using ThermoBet.Core;
using ThermoBet.Core.Interfaces;
using ThermoBet.SQLServer.Services;

namespace ThermoBet.Bootstrap
{
    public static class Init
    {
        public static void CreateDbIfNotExists(IServiceProvider services)
        {
            var context = services.GetRequiredService<ThermoBetContext>();
            DbInitializer.Initialize(context);
        }

        public static void ConfigureServices(IServiceCollection services, IConfiguration Configuration)
        {
            // This is the new code.
            var connection = Configuration.GetConnectionString("DefaultDatabase");

            //services.AddDbContext<ThermoBetContext>(options =>
            //options.UseInMemoryDatabase(databaseName: "ThermoBetInMemory"));

            services.AddDbContext<ThermoBetContext>(options =>
                options.UseMySql(connection, b => b.MigrationsAssembly(typeof(ThermoBetContext).Assembly.FullName))
            );

            services.AddScoped<ITournamentService, TournamentService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IDashboardService, DashboardService>();
            services.AddScoped<IAdminAuthentificationService, AdminAuthentificationService>();
            services.AddScoped<IStatsService, StatsService>();
            services.AddScoped<IDataAdministrationService, DataAdministrationService>();
            services.AddScoped<IConfigurationService, ConfigurationService>();


        }
    }
}
