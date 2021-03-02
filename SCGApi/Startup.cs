using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Microsoft.AspNetCore.Cors;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;

namespace SCGApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDependencyInjections();
            services.AddJWTAuthentication(Configuration);

            services.AddCors();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
            .AddJsonOptions(options =>
                {
                    //options.SerializerSettings.Converters.Add(new IsoDateTimeConverter());
                    options.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.None;
                    options.SerializerSettings.Formatting = Formatting.Indented;
                    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                    //options.SerializerSettings.Culture = new CultureInfo("es-DO");
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    //options.SerializerSettings.Error = (sender, args) => throw args.ErrorContext.Error;

            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Resources")),
                RequestPath = new PathString("/Resources")
            });

            app.UseCors(opt => opt
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true)
                .AllowCredentials()
            );

            app.UseAuthentication();

            app.UseMvc();

        }
    }
}
