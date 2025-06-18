namespace SupportSystem.Application.DTOs.SupportTickets
{
    /// <summary>
    /// Model for getting support ticket responses from client requests.
    /// Used in GET endpoint only.
    /// </summary>
    public class TicketResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
    }
}
