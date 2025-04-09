using TaskManager.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseAppLogging(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCustomSwagger();
builder.Services.AddAppServices(builder.Configuration);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAppMiddlewares();
app.MapAppEndpoints();

await app.MigrateDatabaseIfNeeded();
app.Run();

public partial class Program { }
