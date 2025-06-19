# Clean Architecture Sample

This project is structured using **Clean Architecture** principles to enforce strong separation of concerns, testability, and maintainability. The structure follows the **DAIP** layout:

- **Domain**
- **Application**
- **Infrastructure**
- **Presentation**

---

## What is Clean Architecture?

Clean Architecture is a layered architecture pattern that ensures:
- **Independence of frameworks and UI** — (e.g., web, console, mobile)
- **Testability** — business rules can be tested in isolation
- **Maintainability** — code changes don’t ripple across layers
- **Dependency Rule** — code dependencies point **inward**, toward business rules

---

## Project Structure

- /Domain ← Core business models & interfaces
- /Application ← Use cases & service contracts
- /Infrastructure ← DB access, file systems, external APIs (implementation)
- /Presentation ← Web/API/UI layer (controllers, routes, views)


---

## Layer Responsibilities

### Domain
- Core business entities (e.g., `SupportTicket`)
- Value objects and aggregates
- Business rules and interfaces (e.g., `ISupportTicketRepository`)
- No dependencies on anything else

### Application
- Use case classes (`SupportTicketService`)
- DTOs (`CreateTicketRequest`, `TicketResponse`)
- Interfaces to communicate with infrastructure
- Depends **only on Domain**

### Infrastructure
- Implements the interfaces from Application/Domain
- Contains ORM logic, API clients, file IO (`InMemorySupportTicketRepository`)
- Depends on Application and Domain

### Presentation
- UI logic: Web API, MVC, Razor, React, etc. (`SupportTicketController`)
- Calls Application layer via controllers or handlers
- Knows nothing about Infrastructure directly

---

## Dependency Flow

```text
+---------------------------+
| Presentation (UI)         |
+---------------------------+
              ↓
+---------------------------+
| Application (Use Cases)   |
+---------------------------+
              ↓
+---------------------------+
| Domain (Core Logic)       |
+---------------------------+
              ↑
+---------------------------+
| Infrastructure (External) |
+---------------------------+
```

## Benefits
- Unit-test your use cases without a database or web server

- Swap frameworks easily (e.g., move from REST to gRPC or SQLite to PostgreSQL)

- Focus on business logic, not tech glue