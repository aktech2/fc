using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Cymax.Grabber.Entities;
using Cymax.Grabber.Entities.Models.Exceptions;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Cymax.Grabber.CommonUtils
{
    /// <summary>
    /// Helpers for HTTP requests and responses (JSON and XML)
    /// </summary>
    public class HttpHelpers
    {
        /// <summary>
        /// Creates the JSON HTTP content.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public static StringContent CreateJsonHttpContent(object data)
        {
            var serializedData = JsonConvert.SerializeObject(data);
            return new StringContent(serializedData, Encoding.UTF8, "application/json");
        }

        /// <summary>
        /// Creates the XML HTTP content.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public static StringContent CreateXmlHttpContent(object data)
        {
            var serializedData = XmlConvertor.Serialize(data);
            return new StringContent(serializedData, Encoding.UTF8, "application/xml");
        }

        /// <summary>
        /// Gets the HTTP request timeout.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <returns></returns>
        public static int? GetRequestTimeout(IConfiguration configuration)
        {
            var value = configuration.GetSection(Constants.TimeoutConfigurationRootName)?.Get<int>();
            return value == 0 ? null : value;
        }

        /// <summary>
        /// Processes the JSON response.
        /// </summary>
        /// <typeparam name="TResponseModel">The type of the response model.</typeparam>
        /// <param name="response">The response.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Response is null</exception>
        /// <exception cref="Cymax.Grabber.Entities.Models.Exceptions.ApiRequestException"></exception>
        public static async Task<TResponseModel> ProcessApiJsonResponse<TResponseModel>(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                // To avoid allocating response as string
                await using var stream = await response.Content.ReadAsStreamAsync();
                using var reader = new StreamReader(stream);
                using var jsonTextReader = new JsonTextReader(reader);
                var serializer = new JsonSerializer();
                var deserializedResponse = serializer.Deserialize<TResponseModel>(jsonTextReader);
                if (deserializedResponse is null)
                    throw new Exception("Response is null");
                
                return deserializedResponse;
            }

            if (response.StatusCode != HttpStatusCode.BadRequest) 
                throw new ApiRequestException(response.StatusCode);
            
            var message = await response.Content.ReadAsStringAsync();
            throw new ApiRequestException(response.StatusCode, message);
        }

        /// <summary>
        /// Processes the XML response.
        /// </summary>
        /// <typeparam name="TResponseModel">The type of the response model.</typeparam>
        /// <param name="response">The response.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Response is null</exception>
        /// <exception cref="Cymax.Grabber.Entities.Models.Exceptions.ApiRequestException"></exception>
        public static async Task<TResponseModel> ProcessApiXmlResponse<TResponseModel>(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                // To avoid allocating response as string
                await using var stream = await response.Content.ReadAsStreamAsync();
                var deserializedResponse = XmlConvertor.Deserialize<TResponseModel>(stream);
                if (deserializedResponse is null)
                    throw new Exception("Response is null");
                
                return deserializedResponse;
            }

            if (response.StatusCode != HttpStatusCode.BadRequest) 
                throw new ApiRequestException(response.StatusCode);
            
            var message = await response.Content.ReadAsStringAsync();
            throw new ApiRequestException(response.StatusCode, message);
        }
    }
}