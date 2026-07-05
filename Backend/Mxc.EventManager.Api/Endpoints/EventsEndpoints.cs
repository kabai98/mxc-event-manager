using Microsoft.EntityFrameworkCore;
using Mxc.EventManager.Api.Contracts;
using Mxc.EventManager.Api.DTOs;

namespace Mxc.EventManager.Api.Endpoints;

public static class EventsEndpoints
{
	public static IEndpointRouteBuilder MapEventsEndpoints(this IEndpointRouteBuilder app)
	{
		var group = app.MapGroup("/api/events");
					

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

		return events is null
			? Results.NotFound()
			: Results.Ok(events);
	}

	private static async Task<IResult> GetEventById(int id, IEventsService service)
	{
		var eventDto = await service.GetEventByIdAsync(id);

		return eventDto is null
			? Results.NotFound()
			: Results.Ok(eventDto);
	}

	private static async Task<IResult> PutEvent(
		int id,
		EventDto eventDto,
		IEventsService service)
	{
		if (id != eventDto.Id)
			return Results.BadRequest();

		try
		{
			await service.UpdateEvent(eventDto);
		}
		catch (DbUpdateConcurrencyException)
		{
			if (!await service.EventExistsAsync(id))
				return Results.NotFound();

			throw;
		}

		return Results.NoContent();
	}

	private static async Task<IResult> PostEvent(
		EventDto eventDto,
		IEventsService service)
	{
		await service.CreateEvent(eventDto);

		return Results.Created($"/api/events/{eventDto.Id}", eventDto);
	}

	private static async Task<IResult> DeleteEvent(
		int id,
		IEventsService service)
	{
		await service.DeleteEvent(id);

		return Results.NoContent();
	}
}