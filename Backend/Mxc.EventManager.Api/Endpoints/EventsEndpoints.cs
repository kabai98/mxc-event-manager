using Mxc.EventManager.Api.Contracts;
using Mxc.EventManager.Api.DTOs;

namespace Mxc.EventManager.Api.Endpoints;

public static class EventsEndpoints
{
	/// <summary>
	/// Maps the event-related API endpoints.
	/// </summary>
	/// <param name="app">The endpoint route builder used to register routes.</param>
	/// <returns>The configured endpoint route builder.</returns>
	public static IEndpointRouteBuilder MapEventsEndpoints(this IEndpointRouteBuilder app)
	{
		var group = app.MapGroup("/api/events")
					   .RequireAuthorization();


		group.MapGet("/", GetEvents);
		group.MapGet("/{id:int}", GetEventById);
		group.MapPut("/{id:int}", PutEvent);
		group.MapPost("/", PostEvent);
		group.MapDelete("/{id:int}", DeleteEvent);

		return app;
	}

	private static async Task<IResult> GetEvents(IEventsService service)
	{
		var events = await service.GetListAsync();

		return Results.Ok(events);
	}

	private static async Task<IResult> GetEventById(int id, IEventsService service)
	{
		var eventDto = await service.GetEventByIdAsync(id);

		return Results.Ok(eventDto);
	}

	private static async Task<IResult> PutEvent(
		int id,
		EventDto eventDto,
		IEventsService service)
	{
		if (id != eventDto.Id)
		{
			return Results.BadRequest(
				new { error = "Route id and event id do not match." });
		}

		await service.UpdateEvent(eventDto);

		return Results.NoContent();
	}

	private static async Task<IResult> PostEvent(
		EventDto eventDto,
		IEventsService service)
	{
		 var id = await service.CreateEvent(eventDto);

		return Results.Created(
			$"/api/events/{id}",
			new { id });
	}

	private static async Task<IResult> DeleteEvent(
		int id,
		IEventsService service)
	{
		await service.DeleteEvent(id);

		return Results.NoContent();
	}
}