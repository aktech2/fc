using System.Xml.Serialization;

namespace Cymax.Grabber.Entities.Models.Api3.Responses;

[XmlRoot("xml")]
public class Api3Response
{
    [XmlElement("quote")]
    public decimal Quote { get; set; }
}