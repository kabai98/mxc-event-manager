using Microsoft.AspNetCore.Identity;

namespace Mxc.EventManager.Api.Data.Seeders;

/// <summary>
/// Provides methods for seeding identity users.
/// </summary>
public static class IdentitySeeder
{
	/// <summary>
	/// Seeds default identity users into the database if they do not already exist.
	/// </summary>
	/// <param name="userManager">The user manager used to create identity users.</param>
	/// <exception cref="Exception">
	/// Thrown when user creation fails.
	/// </exception>
	public static async Task SeedUsersAsync(UserManager<IdentityUser> userManager)
	{
		var users = new[]
		{
		new { Email = "admin@test.com", Password = "Admin123" },
		new { Email = "user@test.com", Password = "User1234" },
		new { Email = "user@test2.com", Password = "User1234" }
	};

		foreach (var u in users)
		{
			var existingUser = await userManager.FindByEmailAsync(u.Email);

			if (existingUser == null)
			{
				var user = new IdentityUser
				{
					UserName = u.Email,
					Email = u.Email,
					EmailConfirmed = true
				};

				var result =  await userManager.CreateAsync(user, u.Password);

				if (!result.Succeeded)
				{
					var errors = string.Join(", ", result.Errors.Select(e => e.Description));
					throw new Exception($"User creation failed: {errors}");
				}
			}
		}
	}
}