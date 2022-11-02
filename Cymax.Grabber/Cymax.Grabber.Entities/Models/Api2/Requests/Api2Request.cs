﻿using System.Collections.Generic;
using Cymax.Grabber.Entities.Interfaces;

namespace Cymax.Grabber.Entities.Models.Api2.Requests;

public class Api2Request
{
    public string Consignee { get; set; }
        
    public string Consignor { get; set; }
        
    public List<CartonDimension> Cartons { get; set; }
}