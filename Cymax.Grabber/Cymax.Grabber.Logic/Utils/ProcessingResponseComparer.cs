using System.Collections.Generic;
using Cymax.Grabber.Entities.Models.Factory;

namespace Cymax.Grabber.Logic.Utils;

public class ProcessingResponseComparer: IComparer<ProcessingResponse>
{
    // NULL > !IsSuccess > [x.Value compare to y.Value]
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