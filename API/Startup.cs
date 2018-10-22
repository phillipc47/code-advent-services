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
	    private void BindConfiguration(IServiceCollection services)
	    {
		    services.Configure<Infrastructure.ApplicationConfiguration.ConfigurationData>(options => Configuration.GetSection("ConfigurationData").Bind(options));
	    }

		public IConfiguration Configuration { get; private set; }

		public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void Configure(IApplicationBuilder applicationBuilder, IHostingEnvironment environment)
        {
			if (environment.IsDevelopment())
            {
                applicationBuilder.UseDeveloperExceptionPage();
            }
            else
            {
                applicationBuilder.UseHsts();
            }

			Configuration = ConfigurationBuilderConfiguration.Configure(applicationBuilder, environment);

			CorsConfiguration.Configure(applicationBuilder);
			SwaggerConfiguration.Configure(applicationBuilder);

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

	    public void ConfigureServices(IServiceCollection services)
	    {
			BindConfiguration(services);
			CorsConfiguration.Configure(services);

		    IMvcBuilder mvcBuilder = services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
		    JsonFormattingConfiguration.Configure(mvcBuilder);

		    DependencyInjectionConfiguration.Configure(services);
		    SwaggerConfiguration.Configure(services);
	    }
	}
}
