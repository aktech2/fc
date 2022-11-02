namespace Cymax.Grabber.Entities.Interfaces;

public interface IApiManager<TRequestModel>: IBaseApiManager
{
    TRequestModel Data { get; set; }
}