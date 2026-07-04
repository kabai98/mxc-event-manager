using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mxc.EventManager.Api.Contracts;
using Mxc.EventManager.Api.DTOs;

namespace Mxc.EventManager.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class EventController(IEventsService eventsService) : ControllerBase
{
	[HttpGet]
	public async Task<ActionResult<IEnumerable<EventDto>>> GetEvents()
	{
		var eventsDto = await eventsService.GetListAsync();

		return eventsDto is null ? NotFound() : Ok(eventsDto);
	}

	[HttpGet("{id:int}")]
	public async Task<ActionResult<EventDto>> GetEvent(int id)
	{
		var eventDto = await eventsService.GetEventByIdAsync(id);

		return eventDto is null ? NotFound() : Ok(eventDto);
	}

	[HttpPut("{id:int}")]
	public async Task<IActionResult> PutEvent(int id, EventDto eventDto)
	{
		if (id != eventDto.Id)
		{
			return BadRequest();
		}
		try
		{
			await eventsService.UpdateEvent(eventDto);
		}
		catch (DbUpdateConcurrencyException)
		{
			if (!await eventsService.EventExistsAsync(id))
			{
				return NotFound();
			}
			else
			{
				throw;
			}
		}
		return NoContent();
	}

	[HttpPost]
	public async Task<ActionResult<EventDto>> PostEvent(EventDto eventDto)
	{
		await eventsService.CreateEvent(eventDto);
		return CreatedAtAction(nameof(GetEvent), new { id = eventDto.Id }, eventDto);
	}

	[HttpDelete("{id:int}")]
	public async Task<IActionResult> DeleteEvent(int id)
	{
		await eventsService.DeleteEvent(id);
		return NoContent();
	}
}