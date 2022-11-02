using Cymax.Grabber.Logic.Managers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace Cymax.Grabber.Logic.Utils;

public static class ServiceCollectionHelper
{
    public static void AddApiManagers(this IServiceCollection collection)
    {
        IConfiguration configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();
        
        collection.AddLogging(loggingBuilder =>
        {
            loggingBuilder.AddConfiguration(configuration);
            loggingBuilder.AddSimpleConsole(options =>
            {
                options.IncludeScopes = true;
                options.ColorBehavior = LoggerColorBehavior.Enabled;
                options.TimestampFormat = "dd-MM-yyyy HH:mm:ss.ffff ";
            });
        });
        collection.AddHttpClient();
        collection.AddHttpClient<Api1Manager>();
        collection.AddHttpClient<Api2Manager>();
        collection.AddHttpClient<Api3Manager>();
        collection.AddSingleton(configuration);
    }
}