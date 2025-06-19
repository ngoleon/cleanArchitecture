// Imports like java (note to self)
using SupportSystem.Domain.Entities;

namespace SupportSystem.Domain.Interfaces
{
    /// <summary>
    /// Defines the abstraction of actions that can occur with tickets
    /// </summary>
    public interface ISupportTicketRepository
    {
        Task<IEnumerable<SupportTicket>> GetAllAsync();
        Task<SupportTicket?> GetByIdAsync(Guid id);
        Task AddAsync(SupportTicket ticket);
        Task UpdateAsync(SupportTicket ticket);
        Task DeleteAsync(Guid id);
    }
}
