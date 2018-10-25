using API.ConfigurationData.Repositories;
using API.ConfigurationData.Services;
using CheckSum.Validation;
using ConfigurationData.Service;
using Microsoft.Extensions.DependencyInjection;
using ReverseCaptcha;

namespace API.Infrastructure
{
	public static class DependencyInjectionConfiguration
	{
		public static void Configure(IServiceCollection services)
		{
			services.AddSingleton<IReverseCaptchaService, ReverseCaptchaService>();
			services.AddSingleton<INumericValidator, NumericValidator>();

			services.AddScoped<IConfigurationDataService, ConfigurationDataService>();
			services.AddScoped<IConfigurationDataRepository, ConfigurationDataRespository>();

		}
	}
}
