namespace SupportSystem.Domain.Entities
{
    /// <summary>
    /// Data class that represents the support ticket model
    /// </summary>
    public class SupportTicket
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; } = "Open"; // Open, Resolved, etc.
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

        public SupportTicket(string title, string description)
        {
            Title = title;
            Description = description;
        }
    }
}
