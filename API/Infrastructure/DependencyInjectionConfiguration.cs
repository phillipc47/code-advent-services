using API.ConfigurationData.Repositories;
using API.ConfigurationData.Services;
using CheckSum.Services;
using CheckSum.Validators;
using ConfigurationData.Service;
using Microsoft.Extensions.DependencyInjection;
using Passphrase.Services;
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

			services.AddScoped<ICheckSumService, CheckSumService>();
			services.AddScoped<IPassPhraseService, PassPhraseService>();
		}
	}
}
