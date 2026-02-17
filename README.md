# Maintenance & Work Orders API

A portfolio-ready ASP.NET Core Web API for managing assets and maintenance work orders.
This backend is designed to power a React Native mobile app with clean JSON endpoints, clear domain boundaries, and practical service/repository separation.

## Why this project is resume-ready

- **Real-world domain**: assets, work orders, status lifecycle, and maintenance logs.
- **Practical architecture**: Domain → Application → Infrastructure → API layers.
- **Production-minded choices**: MySQL + Dapper, request validation, health check endpoint, Swagger docs.
- **Test coverage starter**: unit tests for service-level business rules.

---

## Tech stack

- **.NET 8 / ASP.NET Core Web API**
- **Dapper** for SQL data access
- **MySQL** (`MySqlConnector`)
- **xUnit + Moq** for unit tests
- **Swagger / OpenAPI** for API exploration

---

## Project structure

```text
Controllers/         # API endpoints
Application/         # Service layer + interfaces
Infrastructure/      # DB connection + SQL repositories
Domain/              # Core models + enums
Contracts/Requests/  # Request DTOs
database/init.sql    # Schema bootstrap script
MaintenanceAPI.Tests # Unit tests
```

---

## API overview

Base URL: `https://localhost:7025`

### Assets

- `GET /api/asset` - list assets
- `GET /api/asset/{id}` - get a specific asset
- `POST /api/asset` - create asset
- `PUT /api/asset/{id}` - update asset
- `DELETE /api/asset/{id}` - delete asset

### Work Orders

- `GET /api/workorder` - list work orders
- `GET /api/workorder/{id}` - get a specific work order
- `POST /api/workorder` - create a work order
- `PUT /api/workorder/{id}/status` - update status (`Open`, `InProgress`, `Closed`)
- `POST /api/workorder/{id}/logs` - append work order log message

### Health

- `GET /api/health` - validates database connectivity

---

## Setup

## 1) Prerequisites

- .NET 8 SDK
- MySQL 8+ (or compatible MariaDB)

## 2) Clone

```bash
git clone https://github.com/<your-username>/MaintainanceAPI.git
cd MaintainanceAPI
```

## 3) Initialize database

Run the SQL script:

```bash
mysql -u root -p < database/init.sql
```

## 4) Configure app settings

Create `appsettings.json` from `appsettings.example.json` and set:

```json
{
  "ConnectionStrings": {
    "MySql": "Server=localhost;Database=maintainance_api;User=root;Password=your_password;"
  }
}
```

## 5) Run API

```bash
dotnet run --project "Maintenance_&_Work_Orders_API.csproj"
```

Swagger UI:

- `https://localhost:7025/swagger`

---

## Testing

```bash
dotnet test
```

