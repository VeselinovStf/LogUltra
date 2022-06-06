using AutoMapper;
using LogUltra.Core.Abstraction.Database;
using LogUltra.Log.API.Data;
using LogUltra.Log.API.DTOs;
using LogUltra.Log.Service.Extensions;
using LogUltra.Log.Service.Models;
using LogUltra.MongoDb.Condigurations;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Host.ConfigureLogging((whbc, logging) =>
{
    logging.ClearProviders()
        .AddLogUltraMongoDbService<LoggingDatabaseSetting>(c =>
        {
            c.LogLevelsRules[Microsoft.Extensions.Logging.LogLevel.Information] = true;
            c.DbSettings = new LogUltraMongoDbSetting()
            {
                ConnectionString = whbc.Configuration.GetSection("LoggingDatabaseSetting").GetSection("ConnectionString").Value,
                DatabaseName = whbc.Configuration.GetSection("LoggingDatabaseSetting").GetSection("DatabaseName").Value,
                LogCollectionName = whbc.Configuration.GetSection("LoggingDatabaseSetting").GetSection("LogCollectionName").Value,
                Source = "LogUltra.UI"
            };
        }, whbc.Configuration);
    logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace)
    .AddConsole();

});

var app = builder.Build();

app.MapPost("api/logs", async (
    [FromBody] GetLogsRequestDTO request,
    [FromServices] ILogService<GetLogsResponseModel> _logService,
    [FromServices] ILogger<Program> _logger,
    [FromServices] IMapper _mapper) =>
    {
        try
        {
            var serviceResponse = await _logService
                .GetAsync(
                    request.SortColumn,
                    request.SortColumnDirection,
                    request.SearchValue,
                    request.Level,
                    request.Source,
                    request.Exception,
                    request.PageSize,
                    request.Skip);

            if (serviceResponse.Success)
            {
                int recordsTotal = serviceResponse.Logs.Count();

                var mappedLogs = _mapper.Map<List<LogResponseDTO>>(serviceResponse.Logs);

                _logger.LogInformation(serviceResponse.Message);

                return Results.Ok(new GetLogsResponseDTO()
                {
                    Data = mappedLogs,
                    Success = true,
                    Message = serviceResponse.Message,
                    RecordsFiltered = recordsTotal,
                    RecordsTotal = recordsTotal
                });
            }

            _logger.LogError(serviceResponse.Message);

            return Results.BadRequest();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);

            return Results.BadRequest();
        }
    }
);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
