using System.Collections.Generic;

namespace Cymax.Grabber.Entities.Models.Api3.Requests;

public class Api3Request
{
    public string Source { get; set; }
        
    public string Destination { get; set; }
        
    public List<Package> Packages { get; set; }
}