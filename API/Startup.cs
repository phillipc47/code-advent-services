using System.Threading.Tasks;
using API.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace API
{
    public class Startup
    {
	    public IConfiguration Configuration { get; }

		public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
	        services.AddCors();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

	        DependencyInjection.Configure(services);
			SwaggerConfiguration.Configure(services);
		}

        public void Configure(IApplicationBuilder applicationBuilder, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                applicationBuilder.UseDeveloperExceptionPage();
            }
            else
            {
                applicationBuilder.UseHsts();
            }

			SwaggerConfiguration.Configure(applicationBuilder);

	        applicationBuilder.UseCors
	        (
		        _ => _
			        .AllowAnyOrigin()
			        .AllowAnyMethod()
			        .AllowAnyHeader()
			        .AllowCredentials()
	        );

			applicationBuilder
				.UseHttpsRedirection()
				.UseMvc()
				.Run(context =>
				{
					// Make the Swagger UI be the default when a Controller Action is not found
					context.Response.Redirect("swagger");
					return Task.CompletedTask;
				});
        }
    }
}
