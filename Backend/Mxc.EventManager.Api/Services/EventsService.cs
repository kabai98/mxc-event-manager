using Microsoft.EntityFrameworkCore;
using Mxc.EventManager.Api.Contracts;
using Mxc.EventManager.Api.Data;
using Mxc.EventManager.Api.DTOs;

namespace Mxc.EventManager.Api.Services
{
	public class EventsService(EventManagerDbContext context) : IEventsService
	{
		public async Task CreateEvent(EventDto eventDto)
		{
			// az ellenőrzés előtte kellene vagy itt valahol Validáció szerver ,de lehet elég itt
			var newEvent = new Event
			{
				Name = eventDto.Name,
				Location = eventDto.Location,
				Country = eventDto.Country,
				Capacity = eventDto.Capacity
			};

			context.Events.Add(newEvent);

			await context.SaveChangesAsync();
		}

		public async Task DeleteEvent(int id)
		{
			var affected = await context.Events
			  .Where(e => e.Id == id)
			  .ExecuteDeleteAsync();

			//if (affected == 0)
			//{
			//	logger.LogWarning("DeleteEvent: nem létező event törlését próbálták. Id: {Id}", id);
			//}
			//else
			//{
			//	logger.LogInformation("Event törölve. Id: {Id}", id);
			//}
		}

		public async Task<EventDto?> GetEventByIdAsync(int id)
		{
			var eventt = await context.Events
				.Where(e => e.Id == id)
				.Select(e => new EventDto
				{
					Id = e.Id,
					Name = e.Name,
					Location = e.Location,
					Country = e.Country,
					Capacity = e.Capacity
				})
				.AsNoTracking()
				.FirstOrDefaultAsync();

			return eventt;
		}

		public async Task<List<EventDto>> GetListAsync()
		{
			var events = await context.Events
				.Select(e => new EventDto
				{
					Id = e.Id,
					Name = e.Name,
					Location = e.Location,
					Country = e.Country,
					Capacity = e.Capacity
				})
				.AsNoTracking()
				.ToListAsync();

			return events;
		}

		public async Task UpdateEvent(EventDto eventDto)
		{
			var @event = await context.Events
				.FindAsync(eventDto.Id) ?? throw new KeyNotFoundException("Event not found");

			@event.Name = eventDto.Name;
			@event.Location = eventDto.Location;
			@event.Capacity = eventDto.Capacity;
			@event.Country = eventDto.Country;

			context.Events.Update(@event);
			await context.SaveChangesAsync();
		}

		public async Task<bool> EventExistsAsync(int id)
		{
			return await context.Events.AnyAsync(e => e.Id == id);
		}
	}
}