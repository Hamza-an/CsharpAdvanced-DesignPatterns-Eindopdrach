using TemperatureMonitor.Api.Patterns.Structural.Adapter;
using TemperatureMonitor.Api.Patterns.Structural.Facade;
using TemperatureMonitor.Api.Hubs;
using TemperatureMonitor.Api.Patterns.Concurrency;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IRawDataAdapter, PicoStringAdapter>();
builder.Services.AddScoped<ITemperatureMonitoringFacade, TemperatureMonitoringFacade>();

builder.Services.AddSingleton<SensorDataQueue>();
builder.Services.AddSignalR();
builder.Services.AddHostedService<TemperatureMonitor.Api.Services.SensorDataProcessorService>();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.WithOrigins("https://climate.dops.tech", "https://climate.weensum.nl", "http://localhost:5173")
                  .AllowCredentials()
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors();
app.UseAuthorization();

app.MapControllers();

app.MapHub<SensorHub>("/sensorHub");

app.Run();