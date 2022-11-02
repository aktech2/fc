using System.Xml.Serialization;

namespace Cymax.Grabber.Entities.Models.Api3.Requests;

public class Package
{
    [XmlElement("width")]
    public decimal Width { get; set; }
    
    [XmlElement("height")]
    public decimal Height { get; set; }
    
    [XmlElement("depth")]
    public decimal Depth { get; set; }
}