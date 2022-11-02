using System;
using System.Threading.Tasks;
using Cymax.Grabber.Entities.Models.Common;

namespace Cymax.Grabber.Entities.Interfaces;

public interface IBaseApiManager
{
    string Name { get; }
    Task<decimal> MakeRequest(CommonRequest request);
}