using Microsoft.EntityFrameworkCore;

namespace Mxc.EventManager.Api.Data;

public static class DatabaseMigrationExtensions
{
	public static void ApplyDatabaseMigrations(this IApplicationBuilder app)
	{
		using var scope = app.ApplicationServices.CreateScope();

		var db = scope.ServiceProvider
			.GetRequiredService<EventManagerDbContext>();

		db.Database.Migrate();
	}
}
