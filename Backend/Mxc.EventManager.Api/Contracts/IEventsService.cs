using Mxc.EventManager.Api.DTOs;

namespace Mxc.EventManager.Api.Contracts;

public interface IEventsService
{
	Task<List<EventDto>> GetListAsync();

	Task<EventDto> GetEventByIdAsync(int id);

	Task CreateEvent(EventDto eventDto);

	Task DeleteEvent(int id);

	Task UpdateEvent(EventDto eventDto);

	Task<bool> EventExistsAsync(int id);
}