using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Cymax.Grabber.Entities;
using Cymax.Grabber.Entities.Models.Api1.Requests;
using Cymax.Grabber.Entities.Models.Api1.Responses;
using Cymax.Grabber.Entities.Models.Api3.Requests;
using Cymax.Grabber.Entities.Models.Api3.Responses;
using Cymax.Grabber.Logic.Utils;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;

namespace Cymax.Grabber.Tests;

/// <summary>
/// Http helpers tests
/// </summary>
[TestFixture]
public class HttpHelpersTest
{
    /// <summary>
    /// Success generation JSON request content test.
    /// </summary>
    [Test(Description = "Tests if JSON request can be successfully generated from object"), Order(1)]
    public void JsonRequestSuccessTest()
    {
        var obj = new Api1Request();
        var request = HttpHelpers.CreateJsonHttpContent(obj);
        
        Assert.IsNotNull(request);
        Assert.IsNotNull(request.Headers);
        Assert.IsNotNull(request.Headers.ContentType);
        Assert.AreEqual("application/json", request.Headers.ContentType.MediaType);
        Assert.AreEqual("utf-8", request.Headers.ContentType.CharSet);
    }

    /// <summary>
    /// Success generation XML request content test.
    /// </summary>
    [Test(Description = "Tests if XML request can be successfully generated from object"), Order(2)]
    public void XmlRequestSuccessTest()
    {
        var obj = new Api3Request();
        var request = HttpHelpers.CreateXmlHttpContent(obj);
        
        Assert.IsNotNull(request);
        Assert.IsNotNull(request.Headers);
        Assert.IsNotNull(request.Headers.ContentType);
        Assert.AreEqual("application/xml", request.Headers.ContentType.MediaType);
        Assert.AreEqual("utf-8", request.Headers.ContentType.CharSet);
    }

    /// <summary>
    /// Success processing JSON response message test.
    /// </summary>
    [Test(Description = "Tests if JSON response can be successfully retrieved from http client"), Order(3)]
    public async Task JsonResponseSuccessTest()
    {
        var obj = new Api1Response()
        {
            Total = 10m
        };

        var response = new HttpResponseMessage()
        {
            StatusCode = HttpStatusCode.OK,
            Content = HttpHelpers.CreateJsonHttpContent(obj)
        };

        var responseObject = await HttpHelpers.ProcessApiJsonResponse<Api1Response>(response);
        
        Assert.IsNotNull(responseObject);
        Assert.AreEqual(obj.Total, responseObject.Total);
    }

    /// <summary>
    /// Success processing XML response message test.
    /// </summary>
    [Test(Description = "Tests if XML response can be successfully retrieved from http client"), Order(4)]
    public async Task XmlResponseSuccessTest()
    {
        var obj = new Api3Response()
        {
            Quote = 10m
        };

        var response = new HttpResponseMessage()
        {
            StatusCode = HttpStatusCode.OK,
            Content = HttpHelpers.CreateXmlHttpContent(obj)
        };

        var responseObject = await HttpHelpers.ProcessApiXmlResponse<Api3Response>(response);
        
        Assert.IsNotNull(responseObject);
        Assert.AreEqual(obj.Quote, responseObject.Quote);
    }

    /// <summary>
    /// Success retrieving Timeout non-zero value test.
    /// </summary>
    [Test(Description = "Tests if HTTP request timeout can be retrieved"), Order(5)]
    public void TimeoutSuccessTest()
    {
        var mockedConfiguration = new Mock<IConfiguration>();
        var mockedSection = new Mock<IConfigurationSection>();
        mockedSection
            .Setup(s => s.Value)
            .Returns("30000");
        mockedConfiguration
            .Setup(s => s.GetSection(Constants.TimeoutConfigurationRootName))
            .Returns(mockedSection.Object);

        var timeout = HttpHelpers.GetRequestTimeout(mockedConfiguration.Object);
        Assert.AreEqual(30000, timeout);
    }

    /// <summary>
    /// Success retrieving Timeout zero value test.
    /// </summary>
    [Test(Description = "Tests if zero HTTP request timeout can be retrieved"), Order(6)]
    public void TimeoutSuccessZeroTest()
    {
        var mockedConfiguration = new Mock<IConfiguration>();
        var mockedSection = new Mock<IConfigurationSection>();
        mockedSection
            .Setup(s => s.Value)
            .Returns("0");
        mockedConfiguration
            .Setup(s => s.GetSection(Constants.TimeoutConfigurationRootName))
            .Returns(mockedSection.Object);

        var timeout = HttpHelpers.GetRequestTimeout(mockedConfiguration.Object);
        Assert.IsNull(timeout);
    }
}