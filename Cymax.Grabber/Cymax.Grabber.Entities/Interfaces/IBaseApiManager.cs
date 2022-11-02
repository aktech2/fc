using System.Threading.Tasks;

namespace Cymax.Grabber.Entities.Interfaces;

public interface IBaseApiManager
{
    Task<decimal> MakeRequest();
}