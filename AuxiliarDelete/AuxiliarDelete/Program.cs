using AuxiliarDelete;
using AuxiliarDelete.Infra.ExternalRequest.Interface;
using AuxiliarDelete.Infra.ExternalRequest;
using Microsoft.AspNetCore.Builder;
using Serilog;
using AuxiliarDelete.Services.Interfaces;
using AuxiliarDelete.Services;

IHost host = Host.CreateDefaultBuilder(args)
    .UseSerilog()
    .UseWindowsService()
    .ConfigureServices(services =>
    {
        services.AddTransient<IExternalRequest, ExternalRequest>();
        services.AddTransient<ICSVHelper, CSVHelper>();
        services.AddTransient<IFornecedoresService, FornecedoresService>();

        services.AddHostedService<Worker>();
    })
    .Build();


var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
.MinimumLevel.Debug()
.MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning)
.Enrich.FromLogContext()
.WriteTo.File(builder.Configuration["LogDefaultPath"], rollingInterval: RollingInterval.Day)
.CreateLogger();

await host.RunAsync();