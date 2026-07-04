using System.ComponentModel.DataAnnotations;

namespace Mxc.EventManager.Api.DTOs;

public class EventDto
{
	public int Id { get; set; }

	[Required]
	public string Name { get; set; } = null!;

	[Required]
	[MaxLength(100)]
	public string Location { get; set; } = null!;

	public string? Country { get; set; }

	[Range(1, int.MaxValue)]
	public int? Capacity { get; set; }
}
