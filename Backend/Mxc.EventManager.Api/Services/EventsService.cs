using Microsoft.EntityFrameworkCore;
using Mxc.EventManager.Api.Contracts;
using Mxc.EventManager.Api.Data;
using Mxc.EventManager.Api.DTOs;
using Mxc.EventManager.Api.Exceptions;

namespace Mxc.EventManager.Api.Services
{
	public class EventsService : IEventsService
	{
		private readonly EventManagerDbContext _context;
		private readonly ILogger<EventsService> _logger;

		public EventsService(
			EventManagerDbContext context,
			ILogger<EventsService> logger)
		{
			_context = context;
			_logger = logger;
		}

		public async Task<int> CreateEvent(EventDto eventDto)
		{
			ValidateEventDto(eventDto);

			_logger.LogInformation("Creating a new event with name {EventName}.", eventDto.Name);

			try
			{
				var newEvent = new Event
				{
					Name = eventDto.Name,
					Location = eventDto.Location,
					Country = eventDto.Country,
					Capacity = eventDto.Capacity
				};

				_context.Events.Add(newEvent);

				await _context.SaveChangesAsync();

				_logger.LogInformation("Event created successfully with Id {EventId}.", newEvent.Id);

				return newEvent.Id;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Failed to create event with name {EventName}.", eventDto.Name);

				throw;
			}
		}

		public async Task DeleteEvent(int id)
		{
			_logger.LogInformation("Deleting event with Id {EventId}.", id);

			try
			{
				var affected = await _context.Events
				  .Where(e => e.Id == id)
				  .ExecuteDeleteAsync();

				if (affected == 0)
				{
					throw new NotFoundException(
						"Event not found");
				}
			

				_logger.LogInformation("Event with Id {EventId} deleted successfully.", id);
			}
			catch (NotFoundException)
			{
				throw;
			}
			catch (Exception ex)
			{
				_logger.LogError(
					ex,
					"Failed to delete event with Id {EventId}.",
					id);

				throw;
			}
		}

		public async Task<EventDto> GetEventByIdAsync(int id)
		{
			_logger.LogInformation("Retrieving event with Id {EventId}.", id);

			var eventt = await _context.Events
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

			if (eventt == null)
				throw new NotFoundException("Event not found");

			return eventt;
		}

		public async Task<List<EventDto>> GetListAsync()
		{
			_logger.LogInformation("Retrieving all events.");

			var events = await _context.Events
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

			_logger.LogInformation("Successfully retrieved {Count} events.", events.Count);

			return events;
		}

		public async Task UpdateEvent(EventDto eventDto)
		{
			ValidateEventDto(eventDto);

			_logger.LogInformation("Updating event with Id {EventId}.", eventDto.Id);
			try
			{
				var @event = await _context.Events
					.FindAsync(eventDto.Id);

				if (@event == null)
				{
					throw new NotFoundException("Event not found");
				}

				@event.Name = eventDto.Name;
				@event.Location = eventDto.Location;
				@event.Capacity = eventDto.Capacity;
				@event.Country = eventDto.Country;

				await _context.SaveChangesAsync();

				_logger.LogInformation("Event with Id {EventId} updated successfully.", eventDto.Id);
			}

			catch (NotFoundException)
			{
				throw;
			}

			catch (Exception ex)
			{
				_logger.LogError(ex, "Failed to update event with Id {EventId}.", eventDto.Id);

				throw;
			}
		}

		public async Task<bool> EventExistsAsync(int id)
		{
			return await _context.Events.AnyAsync(e => e.Id == id);
		}

		private static void  ValidateEventDto(EventDto eventDto)
		{
			if (string.IsNullOrWhiteSpace(eventDto.Name))
			{
				throw new ValidationException(
					"Event name is required.");
			}

			if (string.IsNullOrWhiteSpace(eventDto.Location))
			{
				throw new ValidationException(
					"Event location is required.");
			}

			if (eventDto.Location.Length > 100)
			{
				throw new ValidationException(
					"Event location cannot exceed 100 characters.");
			}

			if (eventDto.Capacity.HasValue && eventDto.Capacity <= 0)
			{
				throw new ValidationException(
					"Event capacity must be a positive number.");
			}
		}
	}
}