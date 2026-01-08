# MovieSpace

Nowoczesne API do zarządzania filmami i osobami z branży filmowej, zbudowane w oparciu o .NET 8, Clean Architecture oraz PostgreSQL. Projekt przygotowany pod rekrutację – szybki start, klarowna architektura, gotowe środowisko Docker.

## Najważniejsze funkcje
- **Filmy**: CRUD, szczegóły, paginacja, średnia ocena, recenzje
- **Gatunki / Kraje produkcji**: dodawanie, przypinanie do filmów
- **Obsada**: osoby, role, przypinanie ról do filmów
- **Użytkownicy**: rejestracja i logowanie (JWT)
- **Swagger**: interaktywna dokumentacja

## Stos technologiczny
- **Platforma**: .NET 8, ASP.NET Core Web API
- **Architektura**: Clean Architecture (Domain, Application, Infrastructure, WebApi)
- **CQRS**: MediatR
- **ORM**: Entity Framework Core + PostgreSQL
- **Mapowanie**: AutoMapper (profile w warstwie Application)
- **Logowanie**: Serilog (konsola)
- **Kontenery**: Docker + docker-compose

## Architektura (warstwy)
- `src/Domain` – model domenowy, zdarzenia, `Result`, błędy
- `src/Application` – przypadki użycia (komendy/zapytania, walidacje, profile AutoMapper)
- `src/Infrastructure` – EF Core, `ApplicationDbContext`, repozytoria, migracje, usługi
- `src/WebApi` – konfiguracja hosta, kontrolery, Swagger, JWT

## Szybki start (Docker)
1) Utwórz plik `.env` w katalogu głównym repozytorium:

```
ASPNETCORE_ENVIRONMENT=Development
# Baza danych
POSTGRES_DB=MovieSpaceDB
POSTGRES_USER=postgres
POSTGRES_PASSWORD=admin
# Connection string dla API (musi wskazywać na serwis bazy z compose)
ConnectionStrings__Default=Host=moviespace.database;Port=5432;Database=MovieSpaceDB;Username=postgres;Password=admin
# JWT
JWT_ISSUER=moviespace
JWT_AUDIENCE=moviespace.api
JWT_KEY=super_secret_dev_key_change_me
```

2) Uruchom kontenery:

```bash
docker compose up -d --build
```

3) API będzie dostępne pod `http://localhost:5000`. Swagger: `http://localhost:5000/swagger`.

Opcjonalnie: PgAdmin

```bash
# W drugim terminalu, jeśli chcesz GUI do bazy
docker compose -f docker-compose.pgadmin.yml up -d
# PgAdmin: http://localhost:8082 (admin@admin.com / admin)
```

Uwagi
- Migrations: obrazy nie uruchamiają automatycznej migracji – jeśli to potrzebne, uruchom EF lokalnie (sekcja niżej) albo dodaj krok migracji na starcie.

## Uruchomienie lokalne (bez Docker)
Wymagania: .NET 8 SDK, PostgreSQL 14+.

1) Skonfiguruj connection string w `src/WebApi/appsettings.json` lub przez zmienną środowiskową `ConnectionStrings__DefaultConnection`.

2) Zastosuj migracje EF (jednorazowo):

```bash
# Z katalogu głównego repo
dotnet tool update --global dotnet-ef
# Aktualizacja bazy (migracje znajdują się w projekcie Infrastructure)
dotnet ef database update -p src/Infrastructure -s src/WebApi -c ApplicationDbContext
```

3) Uruchom API:

```bash
dotnet run --project src/WebApi
```

- API: `https://localhost:8080` (domyślny port aplikacji) lub wg `launchSettings.json`
- Swagger: `/swagger`

## Migracje EF – skróty
- Dodanie migracji:

```bash
dotnet ef migrations add <NazwaMigracji> -p src/Infrastructure -s src/WebApi -c ApplicationDbContext
```

- Zastosowanie migracji:

```bash
dotnet ef database update -p src/Infrastructure -s src/WebApi -c ApplicationDbContext
```

## Struktura repozytorium
```
MovieSpace.sln
src/
  Domain/
  Application/
  Infrastructure/
  WebApi/
containers/
  db-data/        # wolumen Postgresa
Dockerfile        # (w WebApi)
docker-compose.yml
docker-compose.pgadmin.yml
```

## Autoryzacja i testowanie w Swaggerze
- Po rejestracji/zalogowaniu otrzymujesz JWT.
- Kliknij "Authorize" w Swaggerze i wprowadź: `Bearer <token>`.

## Jakość i logowanie
- Serilog loguje do konsoli. Poziomy i sinki w `appsettings.json`.


