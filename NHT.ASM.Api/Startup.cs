using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using NHT.ASM.Bll.ConfigurationHelpers;
using NHT.ASM.Dal;
using NHT.ASM.Helpers;
using NHT.ASM.Infrastructure;

namespace NHT.ASM.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        //This method gets called by the runtime.Use this method to add services to the container.
        public virtual void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                //https://docs.microsoft.com/en-us/ef/core/querying/related-data#related-data-and-serialization
                .AddJsonOptions(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            // .AddJsonOptions(options => options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore);


            var corsBuilder = new CorsPolicyBuilder();
            corsBuilder.AllowAnyHeader();
            corsBuilder.AllowAnyMethod();
            var corsOriginsSection = Configuration.GetSection("CorsOrigins");
            var corsOrigins = corsOriginsSection.Get<string[]>();
            corsBuilder.WithOrigins(corsOrigins);

            corsBuilder.AllowCredentials();
            services.AddCors(options =>
            {
                options.AddPolicy("SiteCorsPolicy", corsBuilder.Build());
            });

            //add context DI
            services.AddScoped<IAsmContext, AsmContext>();

            // resolve business logic dependency
            services.ConfigureLogicDI();

            // resolve repository dependency
            services.ConfigureRepositoryDI();

            //db service
            services.ConfigureDatabaseService(Configuration);

            //API Version
            services.AddApiVersioning(
                o =>
                {
                    o.ReportApiVersions = true;
                    o.AssumeDefaultVersionWhenUnspecified = true;
                    o.DefaultApiVersion = new ApiVersion(1, 0);
                    o.ApiVersionReader = new UrlSegmentApiVersionReader();
                }
            );

            services.AddVersionedApiExplorer(
                options =>
                {
                    // add the versioned api explorer, which also adds IApiVersionDescriptionProvider service
                    // note: the specified format code will format the version as "'v'major[.minor][-status]"
                    options.GroupNameFormat = "'v'VVV";

                    // note: this option is only necessary when versioning by url segment. the SubstitutionFormat
                    // can also be used to control the format of the API version in route templates
                    options.SubstituteApiVersionInUrl = true;
                });
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

            services.AddSwaggerGen(
                options =>
                {
                    // integrate xml comments
                    options.IncludeXmlComments($"{ConfigHelper.GetSolutionBasePath()}\\NHT.ASM.Api\\NHT.ASM.Api.xml", true);
                    options.IncludeXmlComments($"{ConfigHelper.GetSolutionBasePath()}\\NHT.ASM.Models\\NHT.ASM.Models.xml", true);
                    options.DocumentFilter<RemoveEntityModelsFilter>();
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(
                    options =>
                    {
                        foreach (var description in provider.ApiVersionDescriptions)
                        {

                            options.SwaggerEndpoint(
                                $"../swagger/{description.GroupName}/swagger.json",
                                description.GroupName.ToUpperInvariant());

                        }

                    });

            }
            else
            {
                app.UseHsts();
            }

            app.UseCors("SiteCorsPolicy");
            app.UseHttpsRedirection();
            app.UseMvc();

            // migrations and seeds
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ConfigureMigrationAndSeed(Configuration);
            }
        }
    }
}
