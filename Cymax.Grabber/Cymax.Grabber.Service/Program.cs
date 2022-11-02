using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using Cymax.Grabber.Entities.Interfaces;
using Cymax.Grabber.Entities.Models.Api1.Requests;
using Cymax.Grabber.Entities.Models.Api2.Requests;
using Cymax.Grabber.Entities.Models.Api3.Requests;
using Cymax.Grabber.Entities.Models.Common;
using Cymax.Grabber.Logic;
using Cymax.Grabber.Logic.Utils;
using Microsoft.Extensions.DependencyInjection;

namespace Cymax.Grabber.Service
{
    class Program
    {
        static async Task Main(string[] args)
        {
            /*
             * Request urls can be set in appsettings.json
             * Api1RequestUrl for Api1
             * Api2RequestUrl for Api2
             * Api3RequestUrl for Api3
             * 
             * GlobalApiManager.Init is one time operation that must be performed before GlobalApiManager.ProcessRequests
             *
             * For each CommonRequest GlobalApiManager.ProcessRequests create several API Managers and make request to different APIs
             *
             * Each of implemented API Managers cast CommonRequest to model that required for specific API 
             *
             * Response with the cheapest price will always be the first in the result list
             *
            */
            var serviceProvider = CreateProvider();
            var globalApiManager = serviceProvider.GetRequiredService<GlobalApiManager>();
            globalApiManager.Init();

            var request = GetSomeRequest();
            var result = await globalApiManager.ProcessRequest(request);
            
            Console.WriteLine("Results:");
            for (int i = 0; i < result.Count; i++)
            {
                var item = result[i];
                var price = item.IsSuccess 
                    ? item.Value.ToString(CultureInfo.InvariantCulture)
                    : "-";
                var cheapestMarker = item.IsSuccess && i == 0
                    ? "[Cheapest]"
                    : string.Empty;
                Console.WriteLine($"API: {item.ApiManager}\tPrice: {price}\t{cheapestMarker}");
            }

            Console.ReadLine();
        }

        private static IServiceProvider CreateProvider()
        {
            var collection = new ServiceCollection();
            collection.AddApiManagers();
            return collection.BuildServiceProvider();
        }

        private static CommonRequest GetSomeRequest()
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