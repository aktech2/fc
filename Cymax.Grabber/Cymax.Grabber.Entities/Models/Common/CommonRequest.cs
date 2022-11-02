using System.Collections.Generic;

namespace Cymax.Grabber.Entities.Models.Common;

public class CommonRequest
{
    public string From { get; set; }
    
    public string To { get; set; }
    
    public List<CommonPackage> Packages { get; set; }
}