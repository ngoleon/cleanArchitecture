using SupportSystem.Application.Services;
using SupportSystem.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SupportSystem.Application.DTOs.SupportTickets;

namespace Presentation.Controllers;

/// <summary>
/// Handles all API endpoints for managing support tickets.
/// Acts as the presentation layer in Clean Architecture.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class SupportTicketsController : ControllerBase
{
    private readonly SupportTicketService _service;
    private readonly ILogger<SupportTicketsController> _logger;

    /// <summary>
    /// Constructor where dependencies are injected by ASP.NET Core.
    /// </summary>
    /// <param name="service">Application-layer business logic service.</param>
    /// <param name="logger">Typed logger for this controller.</param>
    public SupportTicketsController(SupportTicketService service, ILogger<SupportTicketsController> logger)
    {
        _service = service;
        _logger = logger;
        _logger.LogInformation("Controller constructed with injected SupportTicketService");
    }

    /// <summary>
    /// Get all existing support tickets.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        _logger.LogInformation("GET /api/supporttickets called");
        var tickets = await _service.GetAllAsync();


        var response = tickets.Select(t => new TicketResponse
        {
            Id = t.Id,
            Title = t.Title,
            Status = t.Status.ToString()
        });

        return Ok(response);
    }

    /// <summary>
    /// Get a single support ticket by its unique ID.
    /// </summary>
    /// <param name="id">GUID of the support ticket.</param>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        _logger.LogInformation("GET /api/supporttickets/{Id} called", id);
        var ticket = await _service.GetByIdAsync(id);
        return ticket is null ? NotFound() : Ok(ticket);
    }

    /// <summary>
    /// Create a new support ticket with title and description.
    /// </summary>
    /// <param name="request">Client request containing title and description.</param>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTicketRequest request)
    {
        _logger.LogInformation("POST /api/supporttickets called with title: {Title}", request.Title);
        await _service.CreateAsync(request.Title, request.Description);
        return Ok();
    }

    /// <summary>
    /// Update an existing support ticket.
    /// </summary>
    /// <param name="ticket">Full ticket object with updated fields.</param>
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] SupportTicket ticket)
    {
        _logger.LogInformation("PUT /api/supporttickets called for ticket ID: {Id}", ticket.Id);
        await _service.UpdateAsync(ticket);
        return Ok();
    }

    /// <summary>
    /// Delete a support ticket by ID.
    /// </summary>
    /// <param name="id">GUID of the ticket to delete.</param>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        _logger.LogInformation("DELETE /api/supporttickets/{Id} called", id);
        await _service.DeleteAsync(id);
        return Ok();
    }
}
