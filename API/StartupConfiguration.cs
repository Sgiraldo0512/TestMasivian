namespace Masivian.Roulette
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.OpenApi.Models;
    using System;
    using System.IO;
    using System.Linq;

    public partial class Startup
    {
        protected IConfiguration Configuration { get; }

        protected readonly SwaggerConfiguration swaggerConfiguration;

        protected const string Bearer = "Bearer";



        protected void AddSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(swagger =>
            {
                swagger.DescribeAllParametersInCamelCase();

                swagger.SwaggerDoc(swaggerConfiguration.DocInfoVersion, GetApiInfo);

                swagger.DocInclusionPredicate((docName, description) => true);
                swagger.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());

                var xmlPath = Path.Combine(AppContext.BaseDirectory, $"{typeof(Startup).Assembly.GetName().Name}.XML");
                swagger.IncludeXmlComments(xmlPath);
            });
        }
 
        protected OpenApiContact GetApiContact => new OpenApiContact
        {
            Name = swaggerConfiguration.ContactName,
            Url = swaggerConfiguration.ContactUrl,
            Email = swaggerConfiguration.ContactEmail
        };

        protected OpenApiInfo GetApiInfo => new OpenApiInfo
        {
            Title = swaggerConfiguration.DocInfoTitle,
            Version = swaggerConfiguration.DocInfoVersion,
            Description = swaggerConfiguration.DocInfoDescription,
            Contact = GetApiContact
        };
    }
}