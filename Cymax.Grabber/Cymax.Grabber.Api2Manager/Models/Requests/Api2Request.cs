﻿using System.Collections.Generic;

namespace Cymax.Grabber.Api2Manager.Models.Requests;

/// <summary>
/// Request definition for API 2
/// </summary>
internal class Api2Request
{
    /// <summary>
    /// Gets or sets the consignee address.
    /// </summary>
    /// <value>
    /// The consignee.
    /// </value>
    public string Consignee { get; set; }

    /// <summary>
    /// Gets or sets the consignor address.
    /// </summary>
    /// <value>
    /// The consignor.
    /// </value>
    public string Consignor { get; set; }

    /// <summary>
    /// Gets or sets the cartons.
    /// </summary>
    /// <value>
    /// The cartons.
    /// </value>
    public List<CartonDimension> Cartons { get; set; }
}