using System;
using System.Threading.Tasks;

namespace Cymax.Grabber.Entities.Interfaces;

public interface IBaseApiManager
{
    Type RequestType { get; }
    Task<decimal> MakeRequest(IRequest request);
}