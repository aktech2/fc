using Cymax.Grabber.Api1Manager.Utils;
using Cymax.Grabber.Api2Manager.Utils;
using Cymax.Grabber.Api3Manager.Utils;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace Cymax.Grabber.Logic.Utils;

/// <summary>
/// Extension to register all business logic
/// </summary>
public static class ServiceCollectionHelper
{
    /// <summary>
    /// Adds the API managers.
    /// </summary>
    /// <param name="collection">The collection.</param>
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
        collection.AddApi1Manager();
        collection.AddApi2Manager();
        collection.AddApi3Manager();
        collection.AddSingleton(configuration);
        collection.AddSingleton<GlobalApiManager>();
    }
}