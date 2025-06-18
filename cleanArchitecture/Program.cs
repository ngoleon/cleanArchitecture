using SupportSystem.Application.Services;
using SupportSystem.Domain.Interfaces;
using Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// 3 Lifetimes in DI:
// Singleton → one instance forever
// Scoped → one instance per HTTP request (best for services/repositories)
// Transient → new instance every time it's requested (rarely used)

// When ISupportTicketRepository is called, give an instance of InMemorySupportTicketRepository
builder.Services.AddScoped<ISupportTicketRepository, InMemorySupportTicketRepository>();

// DI: When SupportTicketService is needed, construct it
// and automatically inject an ISupportTicketRepository (which will be InMemorySupportTicketRepository)
builder.Services.AddScoped<SupportTicketService>();

var app = builder.Build();

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
