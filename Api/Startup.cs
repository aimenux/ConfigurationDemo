using System.Text.Json;
using System.Text.Json.Serialization;
using Api.Extensions;
using Lib.Configuration;
using Lib.Services;

namespace Api;

public class Startup
{
    private static readonly JsonNamingPolicy CamelCase = JsonNamingPolicy.CamelCase;
    
    public void ConfigureServices(WebApplicationBuilder builder)
    {
        builder.AddLogging();
        builder.AddSwaggerDoc();
        builder.Services.AddVersioning();
        builder.Services
            .AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = CamelCase;
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(CamelCase));
            });
        builder.Services.AddRouting(options => options.LowercaseUrls = true);
        builder.Services.AddTransient(GetServiceTypeFactory());
        builder.Services.AddSingleton<ConfigurationOptionsService>();
        builder.Services.AddSingleton<ConfigurationOptionsMonitorService>();
        builder.Services.AddScoped<ConfigurationOptionsSnapshotService>();
        builder.Services.Configure<Features>(builder.Configuration.GetSection(nameof(Features)));
        builder.Services.Configure<Settings>(builder.Configuration.GetSection(nameof(Settings)));
    }

    public void Configure(WebApplication app)
    {
        app.UseSwaggerDoc();
        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
    }
    
    private static Func<IServiceProvider, Func<ServiceType, IConfigurationService>> GetServiceTypeFactory()
    {
        return serviceProvider => serviceType =>
        {
            return serviceType switch
            {
                ServiceType.ConfigurationOptionsService => serviceProvider.GetService<ConfigurationOptionsService>(),
                ServiceType.ConfigurationOptionsMonitorService => serviceProvider.GetService<ConfigurationOptionsMonitorService>(),
                ServiceType.ConfigurationOptionsSnapshotService => serviceProvider.GetService<ConfigurationOptionsSnapshotService>(),
                _ => throw new ArgumentOutOfRangeException($"Unexpected service type {serviceType}")
            };
        };
    }
}