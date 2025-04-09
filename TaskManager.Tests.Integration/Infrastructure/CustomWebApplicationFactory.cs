using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace TaskManager.Tests.Integration.Infrastructure;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Testing");
        builder.ConfigureAppConfiguration((context, config) =>
        {
            var overrideSettings = new Dictionary<string, string>
            {
                ["ConnectionStrings:Default"] = "Host=localhost;Port=5432;Database=taskdb;Username=postgres;Password=postgres"
            };

            config.AddInMemoryCollection(overrideSettings!);
        });
    }
}

