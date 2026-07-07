namespace Mxc.EventManager.Api
{
	public static class CorsExtensions
	{
		public const string DevCorsPolicy = "DevCors";
		public static IServiceCollection AddDevCors(this IServiceCollection services)
		{
			services.AddCors(options =>
			{
				options.AddPolicy(DevCorsPolicy, policy =>
				{
					policy
						.AllowAnyOrigin()
						.AllowAnyHeader()
						.AllowAnyMethod();
				});
			});

			return services;
		}
	}
}
