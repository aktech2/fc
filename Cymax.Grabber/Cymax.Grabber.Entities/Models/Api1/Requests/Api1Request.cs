﻿using System.Collections.Generic;
using Newtonsoft.Json;

namespace Cymax.Grabber.Entities.Models.Api1.Requests;

public class Api1Request
{
    [JsonProperty("contact address")]
    public string ContactAddress { get; set; }
        
    [JsonProperty("warehouse address")]
    public string WarehouseAddress { get; set; }
        
    [JsonProperty("package dimensions")]
    public List<PackageDimension> PackageDimensions { get; set; }
}