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

namespace Cymax.Grabber.Logic.Utils
{
    public class HttpHelpers
    {
        public static StringContent CreateJsonRequestBody(object data)
        {
            var serializedData = JsonConvert.SerializeObject(data);
            return new StringContent(serializedData, Encoding.UTF8, "application/json");
        }
        
        public static StringContent CreateXmlRequestBody(object data)
        {
            var serializedData = XmlConvertor.Serialize(data);
            return new StringContent(serializedData, Encoding.UTF8, "application/xml");
        }

        public static int? GetRequestTimeout(IConfiguration configuration)
        {
            var value = configuration.GetSection(Constants.TimeoutConfigurationRootName)?.Get<int>();
            return value == 0 ? null : value;
        }

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