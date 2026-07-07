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


		/// <summary>
		/// Creates a new event and saves it to the database.
		/// </summary>
		/// <param name="eventDto">The event data to create.</param>
		/// <returns>The ID of the created event.</returns>
		/// <exception cref="ValidationException">
		/// Thrown when the event data is invalid.
		/// </exception>
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

		/// <summary>
		/// Deletes an existing event by its ID.
		/// </summary>
		/// <param name="id">The ID of the event to delete.</param>
		/// <exception cref="NotFoundException">
		/// Thrown when the event does not exist.
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

		/// <summary>
		/// Retrieves a single event by its ID.
		/// </summary>
		/// <param name="id">The ID of the event to retrieve.</param>
		/// <returns>The event data as a DTO.</returns>
		/// <exception cref="NotFoundException">
		/// Thrown when the event cannot be found
		public async Task<EventDto> GetEventByIdAsync(int id)
		{
			_logger.LogInformation("Retrieving event with Id {EventId}.", id);

			var @event = await _context.Events
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

			if (@event == null)
				throw new NotFoundException("Event not found");

			return @event;
		}

		/// <summary>
		/// Retrieves all available events.
		/// </summary>
		/// <returns>A list of event DTOs.</returns>
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


		/// <summary>
		/// Updates an existing event with new data.
		/// </summary>
		/// <param name="eventDto">The updated event information.</param>
		/// <exception cref="NotFoundException">
		/// Thrown when the event does not exist.
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