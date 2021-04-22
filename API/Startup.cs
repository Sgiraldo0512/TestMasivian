namespace Masivian.Roulette
{
    using DataAccess.Common.Dao;
    using DataAccess.Common.Interfaces;
    using EasyCaching.Core.Configurations;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Diagnostics;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public partial class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            swaggerConfiguration = new SwaggerConfiguration(Configuration);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddResponseCompression();

            services.AddEasyCaching(options =>
            {
                options.UseRedis(redisConfig =>
                {
                    redisConfig.DBConfig.Endpoints.Add(new ServerEndPoint("127.0.0.1", 6379));
                    redisConfig.DBConfig.AllowAdmin = true;
                },
                    Utilities.Common.Constantes.NAMEREDIS);
            });


            services.AddScoped<IRepository, Repository>();
            services.AddScoped<BusinessRules.Interfaces.IRoulette, BusinessRules.Roulette>();
            

            services.AddControllers()
                    .AddJsonOptions(options => options.JsonSerializerOptions.IgnoreNullValues = true)
                    .AddJsonOptions(options => options.JsonSerializerOptions.MaxDepth = int.MaxValue);

            AddSwagger(services);
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(errorApp =>
                {
                    errorApp.Run(async context =>
                    {
                        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                        context.Response.ContentType = "application/json";

                        var exceptionHandlerPathFeature =
                            context.Features.Get<IExceptionHandlerPathFeature>();
                    });
                });
                app.UseHsts();
            }

            app.UseResponseCompression();

            app.UseCors("CorsPolicy");

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseStaticFiles();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint(swaggerConfiguration.EndpointSwaggerJson, swaggerConfiguration.EndpointDescription);
                options.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
            });
        }
    }
}