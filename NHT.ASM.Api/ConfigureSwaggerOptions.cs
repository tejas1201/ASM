using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace NHT.ASM.Api
{
    /// <summary>
    /// Configuration for Swagger Open API documentation
    /// Source: https://github.com/Microsoft/aspnet-api-versioning/wiki/API-Documentation#aspnet-core
    /// </summary>
    [SuppressMessage("ReSharper", "InheritdocConsiderUsage")]
    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _provider;

        /// <summary>
        /// Instantiates the class
        /// </summary>
        /// <param name="provider"></param>
        public ConfigureSwaggerOptions( IApiVersionDescriptionProvider provider ) => _provider = provider;

        /// <summary>
        /// Set the configuration
        /// </summary>
        /// <param name="options"></param>
        public void Configure( SwaggerGenOptions options )
        {
            foreach (ApiVersionDescription description in _provider.ApiVersionDescriptions )
            {
                options.SwaggerDoc(
                    description.GroupName,
                    new Info
                    {
                        Title = $"ASM API {description.ApiVersion}",
                        Version = description.ApiVersion.ToString()
                    } );
            }
        }
    }
}