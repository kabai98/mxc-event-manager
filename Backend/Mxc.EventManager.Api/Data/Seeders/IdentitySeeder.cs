using Microsoft.AspNetCore.Identity;

namespace Mxc.EventManager.Api.Data.Seeders;

public static class IdentitySeeder
{
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
					EmailConfirmed = true // különben login nem fog menni default beállításokkal
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