using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace API.Infrastructure
{
	public static class ConfigurationBuilderConfiguration
	{
		public static IConfiguration Configure(IApplicationBuilder applicationBuilder, IHostingEnvironment environment)
		{
			var configurationBuilder = new ConfigurationBuilder()
				.SetBasePath(environment.ContentRootPath)
				.AddJsonFile("appsettings.json", true, true)
				.AddJsonFile($"appsettings.{environment.EnvironmentName}.json", true, true)
				.AddJsonFile("ConfigurationData\\Data\\configurationData.json", false, true)
				.AddEnvironmentVariables();

			return configurationBuilder.Build();
		}
	}
}
