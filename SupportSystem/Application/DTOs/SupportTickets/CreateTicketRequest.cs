namespace SupportSystem.Application.DTOs.SupportTickets
{
    /// <summary>
    /// DTO model for creating new support tickets from client requests.
    /// Used in POST endpoint only.
    /// </summary>
    public class CreateTicketRequest
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
