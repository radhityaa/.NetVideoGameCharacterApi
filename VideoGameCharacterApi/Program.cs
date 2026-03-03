using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Scalar.AspNetCore;
using VideoGameCharacterApi.Data;
using VideoGameCharacterApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddScoped<IVideoGameCharacterService, VideoGameCharacterService>();

// Adding health checks middleware
var hc = builder.Services.AddHealthChecks();


// if the application running
hc.AddCheck(
    name: "self-live",
    check: () => HealthCheckResult.Healthy("Application is healthy"),
    tags: new[] { "live" }
);

// if the application can serve requests
hc.AddCheck(
    name: "self-ready",
    check: () => HealthCheckResult.Healthy("Application is ready to serve requests"),
    tags: new[] { "ready" }
);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
