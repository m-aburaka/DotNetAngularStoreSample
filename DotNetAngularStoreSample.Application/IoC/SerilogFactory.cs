using Serilog;

namespace DotNetAngularStoreSample.Application.IoC
{
    /// <summary>
    /// Creates logger
    /// </summary>
    public static class SerilogFactory
    {
        public static ILogger Get()
        {
            var loggerConfig = new LoggerConfiguration();
            loggerConfig = loggerConfig.MinimumLevel.Information()
                .WriteTo.Console();

            return loggerConfig.CreateLogger();
        }
    }
}