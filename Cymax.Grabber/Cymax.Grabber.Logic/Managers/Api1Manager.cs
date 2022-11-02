using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Cymax.Grabber.Entities;
using Cymax.Grabber.Entities.Interfaces;
using Cymax.Grabber.Entities.Models.Api1.Requests;
using Cymax.Grabber.Entities.Models.Api1.Responses;
using Cymax.Grabber.Logic.Utils;
using Microsoft.Extensions.Configuration;

namespace Cymax.Grabber.Logic.Managers
{
    public class Api1Manager: IApiManager<Api1Request>
    {
        public Api1Request Data { get; set; }
        
        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;
        private string _requestUrl => _configuration[Constants.Api1RequestUrlConfigurationRootName];

        public Api1Manager(HttpClient client, IConfiguration configuration)
        {
            _client = client;
            _configuration = configuration;
        }

        public async Task<decimal> MakeRequest()
        {
            var content = HttpHelpers.CreateJsonRequestBody(Data);
            var timeout = HttpHelpers.GetRequestTimeout(_configuration);
            using var tokenSource = timeout.HasValue
                ? new CancellationTokenSource(timeout.Value)
                : new CancellationTokenSource();
            var token = tokenSource.Token;
            var response = await _client.PostAsync(_requestUrl, content, token);
            var data = await HttpHelpers.ProcessApiJsonResponse<Api1Response>(response);
            return data.Total;
        }
    }
}