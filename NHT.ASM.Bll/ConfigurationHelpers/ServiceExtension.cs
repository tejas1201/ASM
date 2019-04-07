using Microsoft.Extensions.DependencyInjection;
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NHT.ASM.Bll.Interfaces;
using NHT.ASM.Bll.Logic;
using NHT.ASM.Dal;
using NHT.ASM.Dal.ExtensionMethods;
using NHT.ASM.Dal.Interfaces;
using NHT.ASM.Dal.Repositories;
using NHT.ASM.Helpers;

namespace NHT.ASM.Bll.ConfigurationHelpers
{
    /// <summary>
    /// Extension class responsible for resolving dependency injection and database configuration. 
    /// </summary>
    public static class ServiceExtension
    {

        /// <summary>
        /// Configure Repository Dependency Injection
        /// </summary>
        /// <param name="services">used to define dependency injection</param>
        public static void ConfigureRepositoryDI(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
        }

        /// <summary>
        /// Configure Business Logic Dependency Injection
        /// </summary>
        /// <param name="services">used to define dependency injection</param>
        public static void ConfigureLogicDI(this IServiceCollection services)
        {

            services.AddScoped<IUserLogic, UserLogic>();
        }

        /// <summary>
        /// Configure migration and data seeding
        /// </summary>
        /// <param name="serviceScope">The <see cref="M:System.IDisposable.Dispose" /> method ends the scope lifetime. Once Dispose
        /// is called, any scoped services that have been resolved from
        /// <see cref="P:Microsoft.Extensions.DependencyInjection.IServiceScope.ServiceProvider" /> will be
        /// disposed.</param>
        /// <param name="configuration">Represents a set of key/value application configuration properties</param>
        public static void ConfigureMigrationAndSeed(this IServiceScope serviceScope, IConfiguration configuration)
        {
            if (!bool.Parse(configuration["UseInMemoryDatabase"]) && bool.Parse(configuration["DbDeleteAndReCreate"]))
            {
                //if you want to start with a fresh DB
                serviceScope.ServiceProvider.GetService<AsmContext>().Database.EnsureDeleted();
                serviceScope.ServiceProvider.GetService<AsmContext>().Database.EnsureCreated();
            }
            else if (!bool.Parse(configuration["UseInMemoryDatabase"]) && !serviceScope.ServiceProvider.GetService<AsmContext>().AllMigrationsApplied() && bool.Parse(configuration["UseMigrationService"]))
            {
                serviceScope.ServiceProvider.GetService<AsmContext>().Database.Migrate();
            }
            //  it will seed tables on a service run from json files if tables empty
            if (bool.Parse(configuration["UseSeedService"]))
                serviceScope.ServiceProvider.GetService<AsmContext>().EnsureSeeded();
        }

        /// <summary>
        /// Configure database service to use
        /// </summary>
        /// <param name="services">The <see cref="M:System.IDisposable.Dispose" /> method ends the scope lifetime. Once Dispose
        /// is called, any scoped services that have been resolved from
        /// <see cref="P:Microsoft.Extensions.DependencyInjection.IServiceScope.ServiceProvider" /> will be
        /// disposed.</param>
        /// <param name="configuration">Represents a set of key/value application configuration properties</param>
        public static void ConfigureDatabaseService(this IServiceCollection services, IConfiguration configuration)
        {
            if (bool.Parse(configuration["UseInMemoryDatabase"]))
            {
                services.AddDbContext<AsmContext>(opt => opt.UseInMemoryDatabase("AsmTestDB"));
            }
            else
            {
                var connectionString = ConfigHelper.GetConnectionString();

                if (string.IsNullOrEmpty(connectionString))
                    throw new ArgumentNullException($"{nameof(connectionString)} is null");

                services.AddDbContext<AsmContext>(options => options.UseSqlServer(connectionString));
            }
        }
    }
}
