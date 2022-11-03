using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Cymax.Grabber.Api2Manager.Models.Requests;
using Cymax.Grabber.Api2Manager.Models.Responses;
using Cymax.Grabber.CommonUtils;
using Cymax.Grabber.Entities.Interfaces;
using Cymax.Grabber.Entities.Models.Common;
using Microsoft.Extensions.Configuration;

namespace Cymax.Grabber.Api2Manager;

/// <summary>
/// API Manager for API 2
/// </summary>
/// <seealso cref="Cymax.Grabber.Entities.Interfaces.IBaseApiManager" />
internal class Api2Manager: IBaseApiManager
{
    /// <summary>
    /// Gets the name.
    /// </summary>
    /// <value>
    /// The name.
    /// </value>
    public string Name => "API2";

    /// <summary>
    /// The HTTP client
    /// </summary>
    private readonly HttpClient _client;

    /// <summary>
    /// The application configuration
    /// </summary>
    private readonly IConfiguration _configuration;

    /// <summary>
    /// Gets the request URL.
    /// </summary>
    /// <value>
    /// The request URL.
    /// </value>
    private string _requestUrl => _configuration[Api2RequestUrlConfigurationRootName];
    
    /// <summary>
    /// The API2 request URL configuration root name
    /// </summary>
    private const string Api2RequestUrlConfigurationRootName = "Api2RequestUrl";


    /// <summary>
    /// Initializes a new instance of the <see cref="Api2Manager"/> class.
    /// </summary>
    /// <param name="client">The client.</param>
    /// <param name="configuration">The configuration.</param>
    public Api2Manager(HttpClient client, IConfiguration configuration)
    {
        _client = client;
        _configuration = configuration;
    }

    /// <summary>
    /// Makes the request to specific API.
    /// </summary>
    /// <param name="request">The request that should be processed.</param>
    /// <returns></returns>
    public async Task<decimal> MakeRequest(CommonRequest request)
    {
        var mapped = new Api2Request()
        {
            Consignor = request.From,
            Consignee = request.To,
            Cartons = request.Packages.Select(s => new CartonDimension()
            {
                Width = s.Width,
                Height = s.Height,
                Depth = s.Depth
            }).ToList()
        };
        var content = HttpHelpers.CreateJsonHttpContent(mapped);
        var timeout = HttpHelpers.GetRequestTimeout(_configuration);
        using var tokenSource = timeout.HasValue
            ? new CancellationTokenSource(timeout.Value)
            : new CancellationTokenSource();
        var token = tokenSource.Token;
        var response = await _client.PostAsync(_requestUrl, content, token);
        var data = await HttpHelpers.ProcessApiJsonResponse<Api2Response>(response);
        return data.Amount;
    }
}