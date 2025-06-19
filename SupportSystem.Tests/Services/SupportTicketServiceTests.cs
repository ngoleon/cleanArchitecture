using Xunit;
using Moq;
using SupportSystem.Domain.Interfaces;
using SupportSystem.Application.Services;
using SupportSystem.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace SupportSystem.Tests.Services
{

    public class SupportTicketServiceTests
    {
        private readonly Mock<ISupportTicketRepository> _mockRepo;
        private readonly Mock<ILogger<SupportTicketService>> _mockLogger;
        private readonly SupportTicketService _service;

        public SupportTicketServiceTests()
        {
            _mockRepo = new Mock<ISupportTicketRepository>();
            _mockLogger = new Mock<ILogger<SupportTicketService>>();
            _service = new SupportTicketService(_mockRepo.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task CreateAsync_ShouldCallAddAsync_WithCorrectTicket()
        {
            // Arrange
            var title = "Test Ticket";
            var description = "Test Description";

            // Act
            await _service.CreateAsync(title, description);

            // Assert
            _mockRepo.Verify(repo =>
                repo.AddAsync(It.Is<SupportTicket>(t =>
                    t.Title == title && t.Description == description)), Times.Once);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllTickets()
        {
            // Arrange
            var tickets = new List<SupportTicket> { new("Test", "Test Desc") };
            _mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(tickets);

            // Act
            var result = await _service.GetAllAsync();

            // Assert
            Assert.Single(result);
            Assert.Equal("Test", result.First().Title);
        }
    }

}
