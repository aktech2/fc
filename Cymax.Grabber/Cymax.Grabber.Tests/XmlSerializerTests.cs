using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Cymax.Grabber.Entities.Models.Api3.Requests;
using Cymax.Grabber.Logic.Utils;
using NUnit.Framework;

namespace Cymax.Grabber.Tests;

/// <summary>
/// XML serializer/deserializer tests
/// </summary>
[TestFixture]
public class XmlSerializerTests
{
    /// <summary>
    /// Serialization sample object.
    /// </summary>
    private static readonly Api3Request Object = new Api3Request()
    {
        Source = "Start",
        Destination = "Finish",
        Packages = new List<Package>()
        {
            new Package()
            {
                Width = 1,
                Height = 2,
                Depth = 3
            }
        }
    };
    /// <summary>
    /// Sample object serialized XML <see cref="XmlSerializerTests.XmlSerializationSuccessTest"/>
    /// </summary>
    private string _serializedXml;

    /// <summary>
    /// XML serialization success test.
    /// </summary>
    [Test(Description = "Tests if data can be successfully serialized to string"), Order(1)]
    public void XmlSerializationSuccessTest()
    {
        _serializedXml = XmlConvertor.Serialize(Object);
        Assert.IsNotNull(_serializedXml, "Serialized string is null");

        var rootTagAttribute = (XmlRootAttribute) typeof(Api3Request)
            .GetCustomAttributes(typeof(XmlRootAttribute), false)
            .FirstOrDefault();
        var rootTagName = rootTagAttribute?.ElementName;
        
        Assert.IsNotNull(rootTagName, "Root tag name attribute is missing");
            
        var contains = _serializedXml.Contains($"<{rootTagName}>") &&
                       _serializedXml.Contains($"</{rootTagName}>");
        Assert.IsTrue(contains, "Result string does not contains root tag");
    }

    /// <summary>
    /// XML deserialization from string success test.
    /// </summary>
    [Test(Description = "Tests if data can be successfully deserialized from string"), Order(2)]
    public void XmlDeserializationSuccessTest()
    {
        var deserialized = XmlConvertor.Deserialize<Api3Request>(_serializedXml);
        Assert.AreEqual(Object.Source, deserialized.Source);
    }

    /// <summary>
    /// XML deserialization from stream success test.
    /// </summary>
    [Test(Description = "Tests if data can be successfully deserialized from stream"), Order(3)]
    public void XmlDeserializationSuccessStreamTest()
    {
        using Stream stream = new MemoryStream(Encoding.UTF8.GetBytes(_serializedXml));
        var deserialized = XmlConvertor.Deserialize<Api3Request>(stream);
        Assert.AreEqual(Object.Source, deserialized.Source);
    }

    /// <summary>
    /// XML deserialization from corrupted string with expected fail test.
    /// </summary>
    [Test(Description = "Tests if corrupted data can not be successfully deserialized from string"), Order(4)]
    public void XmlDeserializationFailCorruptedTest()
    {
        Assert.Catch<InvalidOperationException>(
            () => XmlConvertor.Deserialize<Api3Request>(_serializedXml.Substring(0, 1))
        );
    }
}