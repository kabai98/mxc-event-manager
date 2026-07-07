namespace Mxc.EventManager.Api
{
	public static class CorsExtensions
	{
		public const string DevCorsPolicy = "DevCors";

		/// <summary>
		/// Adds the development CORS policy configuration.
		/// </summary>
		/// <param name="services">The service collection to add the CORS configuration to.</param>
		/// <returns>The configured service collection.</returns>
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
