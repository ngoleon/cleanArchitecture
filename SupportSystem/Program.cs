using SupportSystem.Application.Services;
using SupportSystem.Domain.Interfaces;
using Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// 3 Lifetimes in DI:
// Singleton → one instance forever
// Scoped → one instance per HTTP request (best for services/repositories)
// Transient → new instance every time it's requested (rarely used)

// Registers metadata for the Swagger generator
builder.Services.AddEndpointsApiExplorer();

// Registers what generates the Swagger docs for controllers and endpoints
builder.Services.AddSwaggerGen();

// When ISupportTicketRepository is called, give an instance of InMemorySupportTicketRepository
builder.Services.AddScoped<ISupportTicketRepository, InMemorySupportTicketRepository>();

// DI: When SupportTicketService is needed, construct it
// and automatically inject an ISupportTicketRepository (which will be InMemorySupportTicketRepository)
builder.Services.AddScoped<SupportTicketService>();

var app = builder.Build();

// Only show in dev mode
if (app.Environment.IsDevelopment())
{
    // Generates swagger.json
    app.UseSwagger();

    // Shows the interactive UI at /swagger
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
