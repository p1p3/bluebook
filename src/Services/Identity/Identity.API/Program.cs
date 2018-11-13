using Identity.Core;
using Identity.Core.Infrastructure.Database.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace Identity.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var webHost = CreateWebHostBuilder(args)
                   .Build();

            using (var scope = webHost.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                Initializer.InitializeIdentityStorages(services, _clientsConfiguration);
            }

            webHost.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .ConfigureLogging((hostingContext, builder) =>
                {
                    builder.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                    builder.AddConsole();
                    builder.AddDebug();
                });

        private static List<ClientConfiguration> _clientsConfiguration => new List<ClientConfiguration>
        {
             new ClientConfiguration()
                {
                    Id = "js",
                    Name = "Angular SPA OpenId Client",
                    AllowedCorsOrigins = new[] { "http://localhost:5090" },
                    PostLogoutRedirectUris = new[] { "http://localhost:50903/index.html" },
                    RedirectUris = new[] { "http://localhost:50903/callback.html" }
                }
        };
    }
}
