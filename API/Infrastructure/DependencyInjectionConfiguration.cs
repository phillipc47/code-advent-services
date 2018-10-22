using API.ReverseCaptcha;
using Microsoft.Extensions.DependencyInjection;

namespace API.Infrastructure
{
	public static class DependencyInjectionConfiguration
	{
		public static void Configure(IServiceCollection services)
		{
			services.AddSingleton<IReverseCaptchaService, ReverseCaptchaService>();
		}
	}
}
