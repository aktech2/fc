using System.Collections.Generic;
using System.Xml.Serialization;
using Cymax.Grabber.Entities.Interfaces;

namespace Cymax.Grabber.Entities.Models.Api3.Requests;

[XmlRoot("xml")]
public class Api3Request: IRequest
{
    [XmlElement("source")]
    public string Source { get; set; }
    
    [XmlElement("destination")]
    public string Destination { get; set; }
    
    [XmlArray("packages")]
    [XmlArrayItem(typeof(Package), ElementName = "package")]
    public List<Package> Packages { get; set; }
}