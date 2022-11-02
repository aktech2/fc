using System;
using System.Collections.Generic;
using Cymax.Grabber.Entities.Models.Api1.Requests;
using Cymax.Grabber.Entities.Models.Api2.Requests;
using Cymax.Grabber.Entities.Models.Api3.Requests;
using Cymax.Grabber.Entities.Models.Factory;
using Cymax.Grabber.Logic.Utils;
using NUnit.Framework;

namespace Cymax.Grabber.Tests;

[TestFixture]
public class ComparerTests
{
    [Test(Description = "Tests if ProcessingResponse can be correct sorted according to comparer"), Order(1)]
    public void ComparerSuccessTest()
    {
        var list = new List<ProcessingResponse>()
        {
            null,
            new ProcessingResponse("API1", 10m),
            new ProcessingResponse("API2", 9m),
            new ProcessingResponse("API3", new Exception())
        };
        list.Sort(new ProcessingResponseComparer());
        
        Assert.AreEqual(9m, list[0].Value);
        Assert.AreEqual(10m, list[1].Value);
        Assert.IsFalse(list[2].IsSuccess);
        Assert.IsNull(list[3]);
    }
}