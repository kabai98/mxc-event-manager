using Microsoft.AspNetCore.Identity;

namespace Mxc.EventManager.Api.Services;

public class IdentitySeederHostedService : IHostedService
{
	private readonly IServiceProvider _serviceProvider;

	public IdentitySeederHostedService(IServiceProvider serviceProvider)
	{
		_serviceProvider = serviceProvider;
	}

	public async Task StartAsync(CancellationToken cancellationToken)
	{
		using var scope = _serviceProvider.CreateScope();

		var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

		await IdentitySeeder.SeedUsersAsync(userManager);
	}

	public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}