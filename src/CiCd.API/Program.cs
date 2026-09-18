using System.Text.Json;

using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHealthChecks();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

// Health Check Endpoints
app.MapHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = async (context, report) =>
    {
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync(JsonSerializer.Serialize(new
        {
            status = report.Status.ToString(),
            service = "CiCd.API",
            duration = report.TotalDuration.TotalMilliseconds,
            environment = context.RequestServices.GetRequiredService<IWebHostEnvironment>().EnvironmentName,
            checks = report.Entries.Select(e => new
            {
                name = e.Key,
                status = e.Value.Status,
                description = e.Value.Description
            })
        }));
    }
});


app.Run();

public partial class Program;
