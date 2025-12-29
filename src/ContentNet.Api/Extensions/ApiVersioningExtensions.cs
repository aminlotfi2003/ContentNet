using Asp.Versioning;

namespace ContentNet.Api.Extensions;

public static class ApiVersioningExtensions
{
    public static IServiceCollection AddApplicationApiVersioning(this IServiceCollection services)
    {
        services
            .AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;

                options.ApiVersionReader = ApiVersionReader.Combine(
                    new UrlSegmentApiVersionReader()
                );

                options.ReportApiVersions = true;
            })
            .AddMvc()
            .AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV"; // v1, v1.0 ...
                options.SubstituteApiVersionInUrl = true;
            });

        return services;
    }
}
