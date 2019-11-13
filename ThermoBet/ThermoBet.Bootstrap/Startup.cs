using System;
using ThermoBet.Data;
using ThermoBet.Data.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using ThermoBet.Core;

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
            services.AddDbContext<ThermoBetContext>(options =>
                //options.UseSqlServer(connection)
                //options.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=EFProviders.InMemory;Trusted_Connection=True;ConnectRetryCount=0")
                //options.UseInMemoryDatabase(databaseName: "ThermoBetInMemory")
                options.UseMySql(connection)
                );

            services.AddScoped<ITournamentService, TournamentService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAdminAuthentificationService, AdminAuthentificationService>();
        }
    }
}
