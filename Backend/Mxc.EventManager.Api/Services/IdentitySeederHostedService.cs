using Microsoft.AspNetCore.Identity;
using Mxc.EventManager.Api.Data.Seeders;

namespace Mxc.EventManager.Api.Services;

public class IdentitySeederHostedService : IHostedService
{
	private readonly IServiceProvider _serviceProvider;
	private readonly ILogger<IdentitySeederHostedService> _logger;

	public IdentitySeederHostedService(IServiceProvider serviceProvider, ILogger<IdentitySeederHostedService> logger)
	{
		_serviceProvider = serviceProvider;
		_logger = logger;
	}

	/// <summary>
	/// Starts the hosted service and seeds default identity users.
	/// </summary>
	/// <param name="cancellationToken">A token used to signal cancellation.</param>
	public async Task StartAsync(CancellationToken cancellationToken)
	{
		try
		{
			using var scope = _serviceProvider.CreateScope();

			var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

			_logger.LogInformation("Starting Identity user seeding...");

			await IdentitySeeder.SeedUsersAsync(userManager);

			_logger.LogInformation("Identity user seeding completed successfully.");
		}

		catch (Exception ex)
		{
			_logger.LogError(ex, "Identity user seeding failed.");
			throw;
		}
	}

	/// <summary>
	/// Stops the hosted service.
	/// </summary>
	/// <param name="cancellationToken">A token used to signal cancellation.</param>

	public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}