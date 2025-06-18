using SupportSystem.Domain.Entities;
using SupportSystem.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Repositories;

/// <summary>
/// In-memory implementation of the support ticket repository.
/// Used as a stand-in for database operations (e.g., EF Core, Dapper).
/// Belongs to the Infrastructure layer in Clean Architecture.
/// </summary>
public class InMemorySupportTicketRepository : ISupportTicketRepository
{
    private readonly ILogger<InMemorySupportTicketRepository> _logger;

    // Internal storage for support tickets — simulates a database
    private readonly List<SupportTicket> _tickets = new();

    /// <summary>
    /// Constructor that receives a typed logger via dependency injection.
    /// </summary>
    /// <param name="logger">Logger for tracing repository operations.</param>
    public InMemorySupportTicketRepository(ILogger<InMemorySupportTicketRepository> logger)
    {
        _logger = logger;
        _logger.LogInformation("InMemorySupportTicketRepository constructed");
    }

    /// <summary>
    /// Get all support tickets currently in memory.
    /// </summary>
    public Task<IEnumerable<SupportTicket>> GetAllAsync()
    {
        _logger.LogInformation("GetAllAsync: Returning {Count} tickets", _tickets.Count);
        return Task.FromResult(_tickets.AsEnumerable());
    }

    /// <summary>
    /// Get a specific support ticket by its unique ID.
    /// </summary>
    /// <param name="id">The ticket's GUID.</param>
    public Task<SupportTicket?> GetByIdAsync(Guid id)
    {
        var ticket = _tickets.FirstOrDefault(t => t.Id == id);
        _logger.LogInformation("GetByIdAsync: {Id} found = {Found}", id, ticket is not null);
        return Task.FromResult(ticket);
    }

    /// <summary>
    /// Add a new support ticket to the in-memory list.
    /// </summary>
    /// <param name="ticket">The ticket entity to add.</param>
    public Task AddAsync(SupportTicket ticket)
    {
        _logger.LogInformation("AddAsync: Adding ticket with ID {Id}", ticket.Id);
        _tickets.Add(ticket);
        return Task.CompletedTask;
    }

    /// <summary>
    /// Update an existing support ticket's data (title, description, status).
    /// </summary>
    /// <param name="ticket">The updated ticket data.</param>
    public Task UpdateAsync(SupportTicket ticket)
    {
        _logger.LogInformation("UpdateAsync: Updating ticket {Id}", ticket.Id);
        var existing = _tickets.FirstOrDefault(t => t.Id == ticket.Id);
        if (existing is not null)
        {
            existing.Title = ticket.Title;
            existing.Description = ticket.Description;
            existing.Status = ticket.Status;
        }
        return Task.CompletedTask;
    }

    /// <summary>
    /// Delete a support ticket by its ID.
    /// </summary>
    /// <param name="id">The ID of the ticket to remove.</param>
    public Task DeleteAsync(Guid id)
    {
        _logger.LogInformation("DeleteAsync: Deleting ticket {Id}", id);
        _tickets.RemoveAll(t => t.Id == id);
        return Task.CompletedTask;
    }
}
