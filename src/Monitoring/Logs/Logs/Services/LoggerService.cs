using Microsoft.Extensions.Logging;
using Serilog.Context;
using System;
using System.Threading.Tasks;

namespace Logs.Services
{
    public class LoggerService
        : ILoggerService
    {
        private readonly ILogger<LoggerService> _logger;

        public LoggerService(ILogger<LoggerService> logger)
        {
            _logger = logger;
        }

        public async Task LogInformation(LoggerRequest request)
        {
            var date = DateTime.UtcNow;
            using (LogContext.PushProperty("ApplicationName", request.ApplicationName))
            using (LogContext.PushProperty("LogMessage", request.Message))
            using (LogContext.PushProperty("Date", date))
            {
                await Task.Run(() => _logger.LogInformation(request.Message));
            }
        }
    }
}
