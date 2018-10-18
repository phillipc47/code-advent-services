using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace API.Infrastructure
{
	public static class SwaggerConfiguration
	{
		public static void Configure(IServiceCollection services)
		{
			services.AddSwaggerGen(configuration =>
				configuration.SwaggerDoc("v1", new Info { Title = "Code Advent Services", Version = "v1" }));
		}

		public static void Configure(IApplicationBuilder applicationBuilder)
		{
			applicationBuilder.UseSwagger();
			applicationBuilder.UseSwaggerUI(configuration =>
				configuration.SwaggerEndpoint("/swagger/v1/swagger.json", "Code Advent Services V1"));
		}
	}
}
