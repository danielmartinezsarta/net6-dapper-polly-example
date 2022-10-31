using DapperPollyExample.Entities;
using Microsoft.Data.Sqlite;
using Dapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("AppointmentsDb") ?? "Data Source=appointments.db";
builder.Services.AddScoped(_ => new SqliteConnection(connectionString));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

await EnsureDb(app.Services, app.Logger);


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


async Task EnsureDb(IServiceProvider appServices, ILogger appLogger)
{
    appLogger.LogInformation("Checking database connection: '{connectionString}'", connectionString);
 
    using var db = appServices.CreateScope().ServiceProvider.GetRequiredService<SqliteConnection>();
    
    var sql = $@"CREATE TABLE IF NOT EXISTS Appointments (
                  {nameof(Appointment.Id)} STRING PRIMARY KEY NOT NULL,
                  {nameof(Appointment.Date)} DATETIME NOT NULL,
                  {nameof(Appointment.ServiceDescription)} STRING NOT NULL,
                  {nameof(Appointment.EstimatedDuration)} DECIMAL(2,2) DEFAULT 0 NOT NULL,
                  {nameof(Appointment.EstimatedPrice)} DECIMAL(2,2) DEFAULT 0 NOT NULL
                 );";
    await db.ExecuteAsync(sql);
}
