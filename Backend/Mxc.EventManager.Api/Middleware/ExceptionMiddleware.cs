using Mxc.EventManager.Api.Exceptions;

namespace Mxc.EventManager.Api.Middleware;

public class ExceptionMiddleware
{
	private readonly ILogger<ExceptionMiddleware> _logger;
	private readonly RequestDelegate _next;

	public ExceptionMiddleware(
		RequestDelegate next,
		ILogger<ExceptionMiddleware> logger)
	{
		_next = next;
		_logger = logger;
	}

	public async Task InvokeAsync(HttpContext context)
	{
		try
		{
			await _next(context);
		}
		catch (NotFoundException ex)
		{
			_logger.LogWarning(
				ex,
				"Resource not found. Path: {Path}",
				context.Request.Path);

			await HandleExceptionAsync(
				context,
				StatusCodes.Status404NotFound,
				ex.Message);
		}
		catch (ValidationException ex)
		{
			_logger.LogWarning(
				ex,
				"Validation failed. Path: {Path}",
				context.Request.Path);

			await HandleExceptionAsync(
				context,
				StatusCodes.Status400BadRequest,
				ex.Message);
		}
		catch (ConflictException ex)
		{
			_logger.LogWarning(
				ex,
				"Conflict occurred. Path: {Path}",
				context.Request.Path);

			await HandleExceptionAsync(
				context,
				StatusCodes.Status409Conflict,
				ex.Message);
		}
		catch (Exception ex)
		{
			_logger.LogError(
				ex,
				"Unhandled exception occurred. Path: {Path}",
				context.Request.Path);

			await HandleExceptionAsync(
				context,
				StatusCodes.Status500InternalServerError,
				"Internal server error");
		}
	}

	private static async Task HandleExceptionAsync(
		HttpContext context,
		int statusCode,
		string message)
	{
		context.Response.StatusCode = statusCode;
		context.Response.ContentType = "application/json";

		await context.Response.WriteAsJsonAsync(new
		{
			error = message
		});
	}

}