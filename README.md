# Maintenance & Work Orders API

A robust RESTful API built with **.NET 8** and **Dapper** for managing industrial assets, vehicle fleets, and maintenance work orders. Designed using **Clean Architecture** principles to ensure scalability and maintainability.

## Features

* **Asset Management**: CRUD operations for tracking machinery and vehicles (Make, Model, Status).
* **Work Order Lifecycle**: Create, update, and track maintenance requests (Open -> InProgress -> Closed).
* **Audit Logging**: Automatic timestamping and status change logs.
* **High Performance**: Uses **Dapper** for raw SQL performance with the safety of parameterized queries.
* **Swagger Documentation**: Integrated OpenAPI UI for testing endpoints.

## Tech Stack

* **Framework**: .NET 8 (C#)
* **Database**: MySQL (compatible with MariaDB/Aurora)
* **ORM**: Dapper (Micro-ORM)
* **Architecture**: Clean Architecture (Separation of Concerns)
* **Testing**: xUnit & Moq
* **Documentation**: Swagger / OpenAPI

## Architecture

The project is organized into strictly defined layers:

* **Domain**: Contains enterprise logic, Enums, and Models (e.g., `Asset`, `WorkOrder`). No dependencies.
* **Application**: Contains business rules and Interfaces (e.g., `IAssetService`). Depends only on Domain.
* **Infrastructure**: Handles database connections and Dapper queries. Implements Application interfaces.
* **API (Controllers)**: The entry point. Handles HTTP requests and responses.

## Getting Started

### Prerequisites
* .NET 8 SDK
* MySQL Server

### Installation

1.  **Clone the repository**
    ```bash
    git clone [https://github.com/yourusername/MaintainanceAPI.git](https://github.com/yourusername/MaintainanceAPI.git)
    ```

2.  **Setup the Database**
    Run the `schema.sql` script located in the root folder on your MySQL instance to create the tables.

3.  **Configure Credentials**
    Rename `appsettings.example.json` to `appsettings.json` and update the connection string:
    ```json
    "ConnectionStrings": {
      "MySql": "Server=localhost;Database=maintainance_api;User=root;Password=your_password;"
    }
    ```

4.  **Run the Application**
    ```bash
    dotnet run --project "Maintenance_&_Work_Orders_API.csproj"
    ```

5.  **Explore the API**
    Open your browser to `https://localhost:7025/swagger` to see the Swagger UI documentation.

## Running Tests

This project uses xUnit for unit testing.
```bash
dotnet test

