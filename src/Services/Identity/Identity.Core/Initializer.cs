using Identity.Core.Infrastructure.Certificates;
using Identity.Core.Infrastructure.Database.Contexts;
using Identity.Core.Infrastructure.IdentityConfiguration;
using Identity.Core.Infrastructure.Database.Extensions;
using Identity.Core.Infrastructure.Database.Models;
using Identity.Core.Models;
using Identity.Core.Services.Abstractions;
using Identity.Core.Services.Implementations;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Reflection;


namespace Identity.Core
{
    public static partial class Initializer
    {

        public static void InitializeIdentityStorages(IServiceProvider serviceProvider, List<ClientConfiguration> clientConfigurations)
        {
            InitializeIdentityStorages(serviceProvider, new ApplicationDbContextSettings(), clientConfigurations);
        }

        public static void InitializeIdentityStorages(IServiceProvider serviceProvider, ApplicationDbContextSettings settings, List<ClientConfiguration> clientConfigurations)
        {
            serviceProvider
           .MigrateDbContext<PersistedGrantDbContext>((_, __) => { })
           .MigrateDbContext<ApplicationDbContext>((context, services) =>
           {
               var env = services.GetService<IHostingEnvironment>();
               var logger = services.GetService<ILogger<ApplicationDbContextSeed>>();

               new ApplicationDbContextSeed()
                   .SeedAsync(context, env, logger, settings)
                   .Wait();
           })
           .MigrateDbContext<ConfigurationDbContext>((context, services) =>
           {
               new ConfigurationDbContextSeed(clientConfigurations)
                   .SeedAsync(context)
                   .Wait();
           });
        }


        public static void InitializeServices(IServiceCollection services, IdentityServicesConfiguration servicesConfiguration)
        {

            var migrationsAssemblyName = typeof(Initializer).GetTypeInfo().Assembly.GetName().Name;

            // Add framework services.
            services.AddDbContext<ApplicationDbContext>(options =>
             options.UseSqlServer(servicesConfiguration.ApplicationConnectionString,
                                     sqlServerOptionsAction: sqlOptions =>
                                     {
                                         sqlOptions.MigrationsAssembly(migrationsAssemblyName);
                                         sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                                     }));

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 3;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();


            // Adds IdentityServer
            services.AddIdentityServer(x =>
            {
                x.IssuerUri = "null";
                x.Authentication.CookieLifetime = TimeSpan.FromHours(2);
            })
            .AddSigningCredential(Certificate.Get())
            .AddAspNetIdentity<ApplicationUser>()
            .AddConfigurationStore(options =>
            {
                options.ConfigureDbContext = builder => builder.UseSqlServer(servicesConfiguration.ConfigurationConnectionString,
                                    sqlServerOptionsAction: sqlOptions =>
                                    {
                                        sqlOptions.MigrationsAssembly(migrationsAssemblyName);
                                        sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                                    });
            })
            .AddOperationalStore(options =>
            {
                options.ConfigureDbContext = builder => builder.UseSqlServer(servicesConfiguration.OperationalConnectionString,
                                sqlServerOptionsAction: sqlOptions =>
                                {
                                    sqlOptions.MigrationsAssembly(migrationsAssemblyName);
                                    sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                                });
            });

            services.AddTransient<IProfileService, ProfileService>();
            services.AddTransient<ILoginService, UserManagerLoginService>();
            services.AddTransient<IRegisterService, UserManagerRegisterService>();
            services.AddTransient<IExternalLoginService, IdentityInteractionsExternalLoginService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public static void InitializeApp(IApplicationBuilder app)
        {
            //app.UseAuthentication()
            app.UseIdentityServer();
        }
    }
}

