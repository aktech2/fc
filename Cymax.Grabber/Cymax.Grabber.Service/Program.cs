using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using Cymax.Grabber.Entities.Models.Common;
using Cymax.Grabber.Entities.Models.Factory;
using Cymax.Grabber.Logic;
using Cymax.Grabber.Logic.Utils;
using Microsoft.Extensions.DependencyInjection;

namespace Cymax.Grabber.Service
{
    /*
     * Request urls should be set in appsettings.json
     * Api1RequestUrl for API 1
     * Api2RequestUrl for API 2
     * Api3RequestUrl for API 3
    */

    /// <summary>
    /// Main execution class
    /// </summary>
    class Program
    {
        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        /// <param name="args">The arguments.</param>
        static async Task Main(string[] args)
        {
            var serviceProvider = CreateProvider();
            var globalApiManager = serviceProvider.GetRequiredService<GlobalApiManager>();
            globalApiManager.Init();

            var request = GetRequestSample();
            var result = await globalApiManager.ProcessRequest(request);
            ShowReport(result);
            Console.ReadLine();
        }
        
        /// <summary>
        /// Renders in console result of <see cref="GlobalApiManager.ProcessRequest"/> method in human friendly form
        /// </summary>
        /// <param name="result">The result of <see cref="GlobalApiManager.ProcessRequest"/> method</param>
        private static void ShowReport(List<ProcessingResponse> result)
        {
            var sb = new StringBuilder();
            sb.AppendLine("Results:");
            sb.AppendLine("API:\tPrice:\tComment:");
            for (int i = 0; i < result.Count; i++)
            {
                var item = result[i];
                sb.Append($"{item.ApiManagerName}\t");
                if (item.IsSuccess)
                    sb.Append(item.Value);
                else
                    sb.Append("-");
                sb.Append('\t');
                if (item.IsSuccess)
                {
                    if (i == 0)
                        sb.AppendLine("[Cheapest]");
                }
                else
                {
                    sb.AppendLine($"Error: {item.Exception.Message}");
                }
            }
            Console.WriteLine(sb.ToString());
        }

        /// <summary>
        /// Creates the service provider.
        /// </summary>
        /// <returns>Dependency injection service provider</returns>
        private static IServiceProvider CreateProvider()
        {
            var collection = new ServiceCollection();
            collection.AddApiManagers();
            return collection.BuildServiceProvider();
        }

        /// <summary>
        /// Gets the request sample.
        /// </summary>
        /// <returns>Sample of <see cref="CommonRequest"/></returns>
        private static CommonRequest GetRequestSample()
        {
            return new CommonRequest()
            {
                From = "My address",
                To = "Another address",
                Packages = new List<CommonPackage>()
                {
                    new CommonPackage()
                    {
                        Width = 10m,
                        Height = 5m,
                        Depth = 0.1m
                    },
                    new CommonPackage()
                    {
                        Width = 5m,
                        Height = 50m,
                        Depth = 1m
                    }
                }
            };
        }
    }
}