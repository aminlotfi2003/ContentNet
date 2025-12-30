# ContentNet API

ContentNet is a layered ASP.NET Core Web API for managing editorial content. It provides OTP-based authentication and a set of article management endpoints with draft, scheduled, published, and archived states.

## Features

- **OTP-based authentication** with JWT issuance.
- **Article management** (create, update, list, publish, schedule, delete/archive).
- **Clean architecture** separation between API, Application, Domain, and Infrastructure layers.
- **API versioning** (URL segment) and **Swagger/OpenAPI** documentation.
- **Global exception handling** returning problem details.

## Solution structure

- `src/ContentNet.Api` — HTTP API layer, controllers, middleware, Swagger, versioning.
- `src/ContentNet.Application` — CQRS handlers, DTOs, validation, and application services.
- `src/ContentNet.Domain` — Core entities, value rules, and domain exceptions.
- `src/ContentNet.Infrastructure` — EF Core persistence, Identity, JWT, OTP, and SMS services.

## Prerequisites

- .NET SDK **9.0**
- SQL Server instance (local, containerized, or managed)

## Configuration

Set the following values (for example in `src/ContentNet.Api/appsettings.Development.json`):

```json
{
  "ConnectionStrings": {
    "Default": "Server=localhost;Database=ContentNet;User Id=sa;Password=Your_password123;TrustServerCertificate=true;"
  },
  "Jwt": {
    "Key": "<strong-random-key>",
    "Issuer": "ContentNet",
    "Audience": "ContentNet"
  }
}
```

> The SMS sender in `ContentNet.Infrastructure` writes OTP codes to the console by default. Replace `SmsSenderService` with a real provider in production.

## Running the API

From the repository root:

```bash
dotnet restore

dotnet ef database update --project src/ContentNet.Infrastructure --startup-project src/ContentNet.Api

dotnet run --project src/ContentNet.Api
```

The API exposes Swagger UI at:

```
https://localhost:<port>/swagger
```

## Authentication flow

1. **Request OTP**
   - `POST /api/v1/auth/request`
   - Body: `{ "phoneNumber": "+15551234567" }`
   - Result: `204 No Content` (OTP is printed to the console by default).

2. **Verify OTP**
   - `POST /api/v1/auth/verify`
   - Body: `{ "phoneNumber": "+15551234567", "code": "123456" }`
   - Result: `200 OK` with `{ "token": "<jwt>" }`

3. **Authorize requests**
   - Set `Authorization: Bearer <jwt>` on protected endpoints.

## Article endpoints (v1)

All article endpoints require a valid JWT.

- `GET /api/v1/articles`
- `GET /api/v1/articles/{id}`
- `POST /api/v1/articles`
- `PUT /api/v1/articles/{id}`
- `PATCH /api/v1/articles/{id}/publish`
- `PATCH /api/v1/articles/{id}/schedule`
- `DELETE /api/v1/articles/{id}` (archives the article)

## Development notes

- Validation is handled via **FluentValidation** and application pipeline behaviors.
- Errors are returned as **problem+json** with trace IDs for debugging.
- API versioning is handled through URL segments (`/api/v{version}/...`).

## License

This project is licensed under the terms of the [MIT License](LICENSE).
