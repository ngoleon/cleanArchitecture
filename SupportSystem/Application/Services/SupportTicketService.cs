using SupportSystem.Domain.Entities;
using SupportSystem.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace SupportSystem.Application.Services;

/// <summary>
/// Application-layer service (business logic) that orchestrates support-ticket use-cases.
/// Depends only on abstractions (ISupportTicketRepository) and never on infrastructure details.
/// </summary>
public class SupportTicketService
{
    private readonly ISupportTicketRepository _repo;
    private readonly ILogger<SupportTicketService> _logger;

    /// <summary>
    /// Constructor called by DI. Injects repository and typed logger.
    /// </summary>
    /// <param name="repo">Infrastructure-layer implementation of <see cref="ISupportTicketRepository"/>.</param>
    /// <param name="logger">Typed logger for this service.</param>
    public SupportTicketService(
        ISupportTicketRepository repo,
        ILogger<SupportTicketService> logger)
    {
        _repo = repo;
        _logger = logger;
        _logger.LogInformation("SupportTicketService constructed with repo type: {RepoType}", repo.GetType().Name);
    }

    /// <summary>
    /// Retrieve all support tickets.
    /// </summary>
    public Task<IEnumerable<SupportTicket>> GetAllAsync()
    {
        _logger.LogInformation("GetAllAsync called");
        return _repo.GetAllAsync();
    }

    /// <summary>
    /// Retrieve a single ticket by its ID.
    /// </summary>
    /// <param name="id">Ticket GUID.</param>
    public Task<SupportTicket?> GetByIdAsync(Guid id)
    {
        _logger.LogInformation("GetByIdAsync called for ID: {Id}", id);
        return _repo.GetByIdAsync(id);
    }

    /// <summary>
    /// Create a new ticket from title + description.
    /// </summary>
    /// <param name="title">Short issue title.</param>
    /// <param name="description">Detailed description.</param>
    public Task CreateAsync(string title, string description)
    {
        _logger.LogInformation("CreateAsync called with title: {Title}", title);
        var ticket = new SupportTicket(title, description);
        return _repo.AddAsync(ticket);
    }

    /// <summary>
    /// Update an existing ticket (title / description / status).
    /// </summary>
    /// <param name="ticket">Ticket entity with new values.</param>
    public Task UpdateAsync(SupportTicket ticket)
    {
        _logger.LogInformation("UpdateAsync called for ID: {Id}", ticket.Id);
        return _repo.UpdateAsync(ticket);
    }

    /// <summary>
    /// Delete a ticket by ID.
    /// </summary>
    /// <param name="id">Ticket GUID.</param>
    public Task DeleteAsync(Guid id)
    {
        _logger.LogInformation("DeleteAsync called for ID: {Id}", id);
        return _repo.DeleteAsync(id);
    }
}
