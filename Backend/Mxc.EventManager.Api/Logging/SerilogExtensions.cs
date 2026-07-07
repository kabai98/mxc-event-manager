using Serilog;

namespace Mxc.EventManager.Api.Logging
{
	public static class SerilogExtensions
	{
		public static void AddSerilogLogging(this WebApplicationBuilder builder)
		{
			Log.Logger = new LoggerConfiguration()
				.ReadFrom.Configuration(builder.Configuration)
				.WriteTo.Console()
				.WriteTo.File(
					"logs/log-.txt",
					rollingInterval: RollingInterval.Day,
					retainedFileCountLimit: 30)
				.CreateLogger();

			builder.Host.UseSerilog();
		}
	}
}
