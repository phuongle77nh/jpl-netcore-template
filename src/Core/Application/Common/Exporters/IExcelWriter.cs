using Jpl.MicroService.Application.Common.Interfaces;

namespace Jpl.MicroService.Application.Common.Exporters;

public interface IExcelWriter : ITransientService
{
    Stream WriteToStream<T>(IList<T> data);
}