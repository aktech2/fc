using System.Collections.Generic;
using Cymax.Grabber.Entities.Models.Factory;

namespace Cymax.Grabber.Logic.Utils;

/// <summary>
/// Comparer class for <see cref="Cymax.Grabber.Entities.Models.Factory.ProcessingResponse" />
/// </summary>
/// <seealso cref="System.Collections.Generic.IComparer&lt;Cymax.Grabber.Entities.Models.Factory.ProcessingResponse&gt;" />
public class ProcessingResponseComparer: IComparer<ProcessingResponse>
{
    /// <summary>
    /// Compares two objects and returns a value indicating whether one is less than, equal to, or greater than the other.
    /// Comparison logic: NULL > !IsSuccess > [x.Value compare to y.Value]
    /// </summary>
    /// <param name="x">The first object to compare.</param>
    /// <param name="y">The second object to compare.</param>
    /// <returns>
    /// A signed integer that indicates the relative values of <paramref name="x" /> and <paramref name="y" />, as shown in the following table.
    /// Value
    /// Meaning
    /// Less than zero
    /// <paramref name="x" /> is less than <paramref name="y" />.
    /// Zero
    /// <paramref name="x" /> equals <paramref name="y" />.
    /// Greater than zero
    /// <paramref name="x" /> is greater than <paramref name="y" />.
    /// </returns>
    public int Compare(ProcessingResponse x, ProcessingResponse y)
    {
        if (x is null && y is not null)
            return 1;
        if (x is not null && y is null)
            return -1;
        if (x is null)
            return 0;

        if (x.IsSuccess && !y.IsSuccess)
            return -1;
        if (!x.IsSuccess && y.IsSuccess)
            return 1;
        if (!x.IsSuccess && !y.IsSuccess)
            return 0;

        if (x.Value > y.Value)
            return 1;
        if (x.Value < y.Value)
            return -1;
        
        return 0;
    }
}