using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Cymax.Grabber.CommonUtils;
using Cymax.Grabber.Tests.Models;
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
    private static readonly SimpleTestModel Object = new SimpleTestModel()
    {
        Source = "Start",
        Destination = "Finish"
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

        var rootTagAttribute = (XmlRootAttribute) typeof(SimpleTestModel)
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
        var deserialized = XmlConvertor.Deserialize<SimpleTestModel>(_serializedXml);
        Assert.AreEqual(Object.Source, deserialized.Source);
    }

    /// <summary>
    /// XML deserialization from stream success test.
    /// </summary>
    [Test(Description = "Tests if data can be successfully deserialized from stream"), Order(3)]
    public void XmlDeserializationSuccessStreamTest()
    {
        using Stream stream = new MemoryStream(Encoding.UTF8.GetBytes(_serializedXml));
        var deserialized = XmlConvertor.Deserialize<SimpleTestModel>(stream);
        Assert.AreEqual(Object.Source, deserialized.Source);
    }

    /// <summary>
    /// XML deserialization from corrupted string with expected fail test.
    /// </summary>
    [Test(Description = "Tests if corrupted data can not be successfully deserialized from string"), Order(4)]
    public void XmlDeserializationFailCorruptedTest()
    {
        Assert.Catch<InvalidOperationException>(
            () => XmlConvertor.Deserialize<SimpleTestModel>(_serializedXml.Substring(0, 1))
        );
    }
}