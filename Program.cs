using Maintenance___Work_Orders_API.Application.Interfaces;
using Maintenance___Work_Orders_API.Application.Services;
using Maintenance___Work_Orders_API.Infrastructure;
using Maintenance___Work_Orders_API.Infrastructure.DB;
using Maintenance___Work_Orders_API.Infrastructure.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IAssetService, AssetService>();
builder.Services.AddScoped<ISqlAssetRepo, SqlAssetRepo>();
builder.Services.AddScoped<IWorkOrderService, WorkOrderService>();
builder.Services.AddScoped<ISqlWorkOrderRepo, SqlWorkOrderRepo>();

builder.Services.AddSingleton<IDbConnectionFactory, MySqlConnectionFactory>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowMobileApp",
        policy =>
        {
            policy.AllowAnyOrigin() // For development only
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowMobileApp");

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
