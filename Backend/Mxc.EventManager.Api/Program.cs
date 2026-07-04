using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Mxc.EventManager.Api;
using Mxc.EventManager.Api.Contracts;
using Mxc.EventManager.Api.Data;
using Mxc.EventManager.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connectionString = builder.Configuration
	.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<EventManagerDbContext>(options =>
	options.UseSqlServer(connectionString));

builder.Services.AddDataProtection();

builder.Services.AddScoped<IEventsService, EventsService>();

builder.Services.AddHostedService<IdentitySeederHostedService>();

builder.Services.AddIdentityApiEndpoints<IdentityUser>()
	.AddEntityFrameworkStores<EventManagerDbContext>();

builder.Services.AddAuthorization();

// identity options configuration
builder.Services.Configure<IdentityOptions>(options =>
{
	options.Password.RequireDigit = true;
	options.Password.RequireLowercase = true;
	options.Password.RequireUppercase = true;
	options.Password.RequireNonAlphanumeric = false;
});

builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

app.MapGroup("api/auth")
	.MapIdentityApi<IdentityUser>();

if (app.Environment.IsDevelopment())
{
	app.MapOpenApi();
}


app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();