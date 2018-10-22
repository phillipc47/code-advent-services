using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace API.Infrastructure
{
	public static class CorsConfiguration
	{
		public static void Configure(IServiceCollection services)
		{
			services.AddCors();
		}

		internal static void Configure(IApplicationBuilder applicationBuilder)
		{
			applicationBuilder.UseCors
			(
				_ => _
					.AllowAnyOrigin()
					.AllowAnyMethod()
					.AllowAnyHeader()
					.AllowCredentials()
			);
		}
	}
}
