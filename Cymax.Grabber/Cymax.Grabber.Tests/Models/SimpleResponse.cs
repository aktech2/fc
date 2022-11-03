using System.Xml.Serialization;

namespace Cymax.Grabber.Tests.Models;

/// <summary>
/// Model to test response retrieving
/// </summary>
[XmlRoot("xml")]
public class SimpleResponse
{
    [XmlElement("total")]
    public decimal Total { get; set; }
}