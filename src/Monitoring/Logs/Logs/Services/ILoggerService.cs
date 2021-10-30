using System.Threading.Tasks;

namespace Logs.Services
{
    public interface ILoggerService
    {
        Task LogInformation(LoggerRequest request);
    }
}