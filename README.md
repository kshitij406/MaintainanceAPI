# Maintenance & Work Orders API

A robust, production-ready ASP.NET Core Web API for managing vehicle fleets, maintenance work orders, fuel logging, and service scheduling. Built with clean Layered Architecture and high-performance data access via Dapper.

---

## Key Features

### Asset Management
- Track vehicles/assets with Make, Model, License Plate, and Odometer details
- Manage asset lifecycles with statuses: `Active`, `Inactive`, and `Maintenance`
- Assign drivers to specific assets

### Work Order System
- Create and manage maintenance tickets (Work Orders)
- **Priority Handling:** `Low`, `Medium`, `High` classification
- **Status Workflow:** `Open` → `In Progress` → `Closed`
- **Activity Logs:** Append timestamped log messages for full audit trails

### Fuel Tracking
- Log fuel entries linking Drivers, Assets, and Costs
- Automatically updates the asset's odometer reading upon fuel entry
- Calculate total costs based on liters and price per liter

### Preventative Maintenance (Service Schedules)
- Schedule recurring services (e.g., "Oil Change", "Tire Rotation")
- **Smart Logic:** Automatically identifies overdue services by date or odometer mileage
- Full history tracking for completed services

### Dashboard & Analytics
Aggregated statistics for the frontend dashboard:
- Total & Active Assets
- Assets currently in Maintenance
- Open & High-Priority Work Orders

---

## Tech Stack

| Layer | Technology |
|---|---|
| Framework | .NET 8 (ASP.NET Core Web API) |
| Language | C# |
| Database | MySQL 8.0+ |
| Data Access | Dapper (Micro-ORM) |
| DB Driver | MySqlConnector |
| Testing | xUnit + Moq |
| Documentation | Swagger / OpenAPI |

---

## Architecture

```text
├── Application/           # Business Logic & Interfaces (Service Layer)
├── Contracts/             # DTOs (Requests & Responses)
├── Controllers/           # API Endpoints (Presentation Layer)
├── Domain/                # Core Entities & Enums
├── Infrastructure/        # Database Repositories & Dapper Implementations
├── MaintenanceAPI.Tests/  # Unit Tests
└── database/              # SQL Initialization Scripts
```

---

## Database Schema

| Table | Description |
|---|---|
| `assets` | Core entity for vehicles |
| `drivers` | Linked to assets (One-to-Many) |
| `work_orders` | Maintenance tickets linked to assets |
| `work_order_logs` | History of actions on a work order |
| `fuel_logs` | Tracks fuel consumption and cost |
| `service_schedules` | Tracks preventative maintenance rules |

---

## Getting Started

### Prerequisites
- [.NET 8.0 SDK](https://dotnet.microsoft.com/download)
- MySQL Server

### 1. Clone the Repository

```bash
git clone https://github.com/yourusername/MaintainanceAPI.git
cd MaintainanceAPI
```

### 2. Database Setup

A SQL script is provided to initialize the schema and seed demo data.

```bash
mysql -u root -p < database/init.sql
```

> This creates the `maintainance_api` database and populates it with sample assets, drivers, and work orders.

### 3. Configuration

Rename `appsettings.example.json` to `appsettings.json` and update your connection string:

```json
{
  "ConnectionStrings": {
    "MySql": "Server=localhost;Port=3306;Database=maintainance_api;User Id=root;Password=your_password;"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

### 4. Run the Application

```bash
dotnet run --project "FleetManagementApi.csproj"
```

### 5. Explore the API

Once running, open Swagger UI to test endpoints interactively:

```
http://localhost:5276/swagger
```

> Port may vary — check your console output.

---

## Testing

Unit tests cover the Service layer using **xUnit** and **Moq**, ensuring business logic validity without hitting the database.

```bash
dotnet test
```

---

## 📡 API Endpoints

| Method | Endpoint | Description |
|---|---|---|
| `GET` | `/api/dashboard` | Get high-level stats for the home screen |
| `GET` | `/api/health` | Check database connectivity |
| `GET` | `/api/asset` | List all assets |
| `POST` | `/api/asset` | Register a new vehicle |
| `PUT` | `/api/asset/{id}/assign-driver/{driverId}` | Assign a driver to a vehicle |
| `GET` | `/api/workorder` | List all maintenance tickets |
| `POST` | `/api/workorder/{id}/logs` | Add a comment/log to a ticket |
| `GET` | `/api/serviceschedule/overdue` | Get list of vehicles with overdue service |
| `POST` | `/api/fuellog` | Log fuel entry & auto-update odometer |
```
