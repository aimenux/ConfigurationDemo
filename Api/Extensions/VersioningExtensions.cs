using Asp.Versioning;

namespace Api.Extensions;

public static class VersioningExtensions
{
    private const string ApiVersionHeaderName = "x-api-version";
    
    public static IServiceCollection AddVersioning(this IServiceCollection services)
    {
        var defaultVersion = new ApiVersion(1, 0);
        services
            .AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = defaultVersion;
                options.ApiVersionReader = GetApiVersionReader();
            })
            .AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = defaultVersion;
                options.ApiVersionParameterSource = GetApiVersionReader();
            });
        return services;
    }

    private static IApiVersionReader GetApiVersionReader()
    {
        return ApiVersionReader.Combine(
            new UrlSegmentApiVersionReader(),
            new QueryStringApiVersionReader(ApiVersionHeaderName),
            new HeaderApiVersionReader(ApiVersionHeaderName),
            new MediaTypeApiVersionReader(ApiVersionHeaderName));
    }
}