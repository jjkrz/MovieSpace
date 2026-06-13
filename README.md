# MovieSpace

REST API for managing movies, cast, reviews, ratings, and collaborative group movie-picking sessions.

## Tech Stack

| Category | Technology |
|---|---|
| Platform | .NET 8, ASP.NET Core Web API |
| Architecture | Clean Architecture (4-layer) |
| CQRS | MediatR |
| ORM | Entity Framework Core 8 + Npgsql |
| Auth | ASP.NET Core Identity + JWT Bearer |
| Validation | FluentValidation (MediatR pipeline behavior) |
| Real-time | SignalR (WebSockets) |
| External API | OMDB HTTP client |
| Logging | Serilog |
| Containers | Docker + docker-compose |
| Unit Tests | xUnit, FluentAssertions |
| Integration Tests | xUnit, Testcontainers (PostgreSQL), WebApplicationFactory |
| CI/CD | GitHub Actions |

## Architecture

```
src/
├── Domain/          # Entities, Result<T> pattern, domain errors
├── Application/     # Commands, Queries, validators, abstractions
├── Infrastructure/  # EF Core, repositories, Identity, JWT, SignalR, OMDB client
└── WebApi/          # Controllers, Swagger, host configuration
```

## Features

- **Movies** — CRUD, pagination, average rating
- **Genres & Production Countries** — add, delete, assign to movies
- **Cast** — people, roles, many-to-many assignments
- **Reviews & Ratings** — add reviews, score movies (JWT protected)
- **Auth** — register, login, JWT token
- **Group Sessions** — real-time collaborative movie-picking via SignalR; match detected when all participants swipe Accept on the same movie

## Tests

**Unit tests** — domain logic for `Movie`, `Genre`, `ProductionCountry`, `Rating`, `Review`, `Session`:
```bash
dotnet test UnitTests/MovieSpace.UnitTests.csproj
```

**Integration tests** — 12 auth flow scenarios against a real PostgreSQL container (Testcontainers), full ASP.NET Core pipeline via WebApplicationFactory:
```bash
dotnet test IntegrationTests/MovieSpace.IntegrationTests.csproj
```

Both suites run automatically on every push/PR to `main` via **GitHub Actions**.

## Quick Start (Docker)

1. Create `.env` in the repo root:

```env
ASPNETCORE_ENVIRONMENT=Development
POSTGRES_DB=MovieSpaceDB
POSTGRES_USER=postgres
POSTGRES_PASSWORD=admin
ConnectionStrings__Default=Host=moviespace.database;Port=5432;Database=MovieSpaceDB;Username=postgres;Password=admin
JWT_ISSUER=moviespace
JWT_AUDIENCE=moviespace.api
JWT_KEY=super_secret_dev_key_change_me
```

2. Start:
```bash
docker compose up -d --build
```

API: `http://localhost:5000` | Swagger: `http://localhost:5000/swagger`

## Quick Start (Local)

Requirements: .NET 8 SDK, PostgreSQL 14+

```bash
dotnet ef database update -p src/Infrastructure -s src/WebApi -c ApplicationDbContext
dotnet run --project src/WebApi
```

## EF Core Migrations

```bash
# Add
dotnet ef migrations add <Name> -p src/Infrastructure -s src/WebApi -c ApplicationDbContext
# Apply
dotnet ef database update -p src/Infrastructure -s src/WebApi -c ApplicationDbContext
```

## Auth (Swagger)

1. `POST /api/auth/register`
2. `POST /api/auth/login` — returns JWT
3. Click **Authorize** → `Bearer <token>`
