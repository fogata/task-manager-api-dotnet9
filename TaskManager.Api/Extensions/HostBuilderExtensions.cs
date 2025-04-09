using Serilog;

namespace TaskManager.Api.Extensions;

public static class HostBuilderExtensions
{
    public static IHostBuilder UseAppLogging(this IHostBuilder host, IConfiguration configuration)
    {
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .Enrich.FromLogContext()
            .CreateLogger();

        return host.UseSerilog();
    }
}
