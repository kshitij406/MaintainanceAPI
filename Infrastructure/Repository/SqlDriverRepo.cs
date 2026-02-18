using Dapper;
using Maintenance___Work_Orders_API.Application.Interfaces;
using Maintenance___Work_Orders_API.Domain.Models;
using Maintenance___Work_Orders_API.Infrastructure.DB;
using Maintenance___Work_Orders_API.Contracts.Requests;

public class SqlDriverRepo : ISqlDriverRepo
{
    private readonly IDbConnectionFactory _connectionFactory;

    public SqlDriverRepo(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public IEnumerable<Driver> GetAllDrivers()
    {
        using var connection = _connectionFactory.CreateConnection();
        connection.Open();

        const string sql = @"
            SELECT 
                id AS Id,
                name AS Name,
                license_number AS LicenseNumber,
                phone_number AS PhoneNumber,
                status AS Status,
                created_at AS CreatedAt
            FROM Drivers;
            ";

        return connection.Query<Driver>(sql);
    }

    public Driver? GetDriverById(int Id)
    {
        using var connection = _connectionFactory.CreateConnection();
        connection.Open();

        const string sql = @"
        SELECT 
              id AS Id,
              name AS Name,
              license_number AS LicenceNumber,
              phone_number AS PhoneNumber,
              status AS Status,
              created_at AS CreatedAt    
        FROM Drivers
        WHERE id = @Id";

        Driver? Driver = connection.QuerySingleOrDefault<Driver>(sql, new { Id = Id });
        return Driver;
    }


    public Driver CreateDriver(CreateDriverRequest request)
    {
        using var connection = _connectionFactory.CreateConnection();
        connection.Open();

        const string insertSql = @"
            INSERT INTO Drivers (name, license_number, phone_number)
            VALUES (@Name, @LicenseNumber, @PhoneNumber);
        ";

        var createdAt = DateTime.UtcNow;

        connection.Execute(insertSql,
            new
            {
                Name = request.Name,
                LicenseNumber = request.LicenseNumber,
                PhoneNumber = request.PhoneNumber,
            });

        const string selectSql = @"
            SELECT 
                id AS Id,
                name AS Name,
                license_number AS LicenseNumber,
                phone_number AS PhoneNumber,
                status AS Status,
                created_at AS CreatedAt
            FROM Drivers
            WHERE license_number = @LicenseNumber
            ORDER BY created_at DESC
            LIMIT 1;
        ";

        var createdDriver = connection.QuerySingleOrDefault<Driver>(selectSql, new { LicenseNumber = request.LicenseNumber });

        if (createdDriver == null)
        {
            throw new InvalidOperationException("Failed to retrieve created Driver.");
        }

        return createdDriver;
    }

    public void UpdateDriver(int Id, UpdateDriverRequest Driver)
    {
        using var connection = _connectionFactory.CreateConnection();
        connection.Open();

        const string sql = @"
            UPDATE Drivers
            SET name = Name,
                name = @Name,
                license_number = @LicenseNumber,
                phone_number = @PhoneNumber,
            WHERE id = @Id
        ";

        connection.Execute(sql,
            new
            {
                id = Id,
                Name = Driver.Name,
                LicenseNumber = Driver.LicenseNumber,
                PhoneNumber = Driver.PhoneNumber,
            });
    }
    public void DeleteDriver(int Id)
    {
        using var connection = _connectionFactory.CreateConnection();
        connection.Open();
        const string sql = "DELETE FROM Drivers WHERE id = @Id";

        connection.Execute(sql,
            new
            {
                Id = Id
            });

    }

}
