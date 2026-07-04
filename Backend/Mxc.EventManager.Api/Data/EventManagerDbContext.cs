using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Mxc.EventManager.Api.Data;

public class EventManagerDbContext : IdentityDbContext<IdentityUser>
{
	public EventManagerDbContext(DbContextOptions<EventManagerDbContext> options) : base(options)

	{ }

	public DbSet<Event> Events => Set<Event>();
}