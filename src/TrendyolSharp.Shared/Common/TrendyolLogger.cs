// using Serilog;
// using Serilog.Events;
// using Serilog.Formatting.Json;
//
// namespace TrendyolSharp.Shared.Common;
//
// public static class TrendyolLogger
// {
//   private static bool _isConfigured = false;
//   /// <summary>
//   /// Configures Serilog logger, initialized when trendyol api client instance created
//   /// </summary>
//   public static void Configure() {
//     if (_isConfigured) return;
//     const string logTemplate = "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}";
//     var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
//     var minLevel = environment == "Development"
//                      ? LogEventLevel.Debug
//                      : LogEventLevel.Information;
//     Log.Logger = new LoggerConfiguration()
//                  .MinimumLevel.Is(minLevel)
//                  .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
//                  .MinimumLevel.Override("System", LogEventLevel.Warning)
//                  .Enrich.FromLogContext()
//                  .WriteTo.Console(outputTemplate: logTemplate)
//                  .WriteTo.File(new JsonFormatter(), "Logs/log.json", rollingInterval: RollingInterval.Day)
//                  .CreateLogger();
//     _isConfigured = true;
//   }
// }