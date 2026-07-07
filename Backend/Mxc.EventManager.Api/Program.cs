using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Mxc.EventManager.Api;
using Mxc.EventManager.Api.Contracts;
using Mxc.EventManager.Api.Data;
using Mxc.EventManager.Api.Endpoints;
using Mxc.EventManager.Api.Logging;
using Mxc.EventManager.Api.Middleware;
using Mxc.EventManager.Api.Services;
using Scalar.AspNetCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDevCors();

builder.AddSerilogLogging();

var connectionString = builder.Configuration
	.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<EventManagerDbContext>(options =>
	options.UseSqlServer(connectionString));

builder.Services.AddDataProtection();

builder.Services.AddScoped<IEventsService, EventsService>();

builder.Services.AddHostedService<IdentitySeederHostedService>();

builder.Services.AddIdentityApiEndpoints<IdentityUser>()
	.AddEntityFrameworkStores<EventManagerDbContext>();

builder.Services.AddAuthentication();
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
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();

var app = builder.Build();

app.ApplyDatabaseMigrations();

app.UseMiddleware<ExceptionMiddleware>();

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseCors(CorsExtensions.DevCorsPolicy);

app.UseAuthentication();

app.UseAuthorization();

app.MapEventsEndpoints();

app.MapGroup("api/auth")
	.MapIdentityApi<IdentityUser>();

if (app.Environment.IsDevelopment())
{
	app.MapOpenApi();
}

app.MapScalarApiReference(options =>
{
	options.Title = "My API";
});

app.MapControllers();

app.Run();